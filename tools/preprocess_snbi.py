#!/usr/bin/env python3
"""
SNBI Specification Preprocessor for BridgeInsight
Downloads the official FHWA "Specifications for the National Bridge Inventory"
(SNBI, Publication No. FHWA-HIF-22-017, March 2022 w/ errata #1) and extracts
section-level text chunks into JSON for the client-side "Ask the SNBI Guide"
retrieval feature.

Output: src/BridgeInsight/wwwroot/data/snbi-chunks.json
Schema: [{"id": ..., "section": ..., "title": ..., "text": ...}]

Coverage: the Introduction, all data item definitions (B.ID.01 through B.W.03,
154 items), section/subsection overviews (which include the component condition
rating code tables), and Appendix C (component condition rating guidance).
Skipped: cover/TOC pages, the comprehensive example bridge (Bridge 15558X)
walkthrough, Appendix A (example data sets), and Appendix B (indexes) — these
are illustrations and lookup aids, not normative specification text. Figures
and some multi-column tables are flattened by PDF text extraction.

Requires: pypdf  (pip install --user pypdf)
"""

import json
import os
import re
import sys
import urllib.request

SNBI_URL = "https://www.fhwa.dot.gov/bridge/snbi/errata1_to_snbi_march_2022_publication.pdf"

# Word-count targets for chunks (roughly 300-600 words each)
MAX_CHUNK_WORDS = 600
MIN_CHUNK_WORDS = 40

# Map item ID prefixes to their SNBI section/subsection names
SUBSECTION_NAMES = {
    "B.ID": "1.1 Identification",
    "B.L": "1.2 Location",
    "B.CL": "1.3 Classification",
    "B.SP": "2.1 Span Material and Type",
    "B.SB": "2.2 Substructure Material and Type",
    "B.RH": "2.3 Roadside Hardware",
    "B.G": "3 Bridge Geometry",
    "B.F": "4.1 Feature Identification",
    "B.RT": "4.2 Routes",
    "B.H": "4.3 Highways",
    "B.RR": "4.4 Railroads",
    "B.N": "4.5 Navigable Waterways",
    "B.LR": "5.1 Loads and Load Rating",
    "B.PS": "5.2 Load Posting Status",
    "B.EP": "5.3 Load Evaluation and Posting",
    "B.IR": "6.1 Inspection Requirements",
    "B.IE": "6.2 Inspection Events",
    "B.C": "7.1 Component Condition Ratings",
    "B.E": "7.2 Element Identification",
    "B.CS": "7.3 Element Conditions",
    "B.AP": "7.4 Appraisal",
    "B.W": "7.5 Work Events",
}

ITEM_ID_RE = re.compile(r"Item ID\s*\n?\s*(B\.[A-Z]{1,3}\.\d{2})")
TOC_LINE_RE = re.compile(r"^(B\.[A-Z]{1,3}\.\d{2})\s+(.+?)\s*\.{2,}\s*(\d+)\s*$")
HEADING_RE = re.compile(r"^(SECTION (\d+)|SUBSECTION (\d+\.\d+)):\s*([A-Z][A-Z .,&()/'’-]+)$")
FOOTER_RE = re.compile(r"March 2022 w/\s*errata\s*#?1")
TOC_MARKER_RE = re.compile(r"TOC\s+(Con\s*densed|Ex\s*panded)")
RUNNING_HEADER_RE = re.compile(r"^\s*(\d+(\.\d+)?\s*[–—-]\s*)?[A-Z0-9 .,&()/'’–—-]+\s*$")


def download_pdf(pdf_path):
    print(f"Downloading SNBI PDF from {SNBI_URL} ...")
    req = urllib.request.Request(SNBI_URL, headers={"User-Agent": "BridgeInsight-preprocessor"})
    with urllib.request.urlopen(req) as resp, open(pdf_path, "wb") as f:
        content_type = resp.headers.get("Content-Type", "")
        if "pdf" not in content_type:
            print(f"Error: URL did not return a PDF (Content-Type: {content_type})")
            sys.exit(1)
        f.write(resp.read())
    print(f"Downloaded {os.path.getsize(pdf_path) / 1024 / 1024:.1f} MB")


def clean_page(text):
    """Strip running headers, footers, and TOC navigation links from a page."""
    lines = text.splitlines()
    cleaned = []
    seen_content = False
    for line in lines:
        stripped = line.strip()
        if not stripped:
            continue
        if FOOTER_RE.search(stripped) or TOC_MARKER_RE.search(stripped):
            continue
        # Drop the ALL-CAPS running section header at the top of each page
        # (e.g. "7.1 - COMPONENT CONDITION RATINGS"), but keep real headings
        # like "SECTION 7: BRIDGE CONDITION".
        if (not seen_content and len(stripped) < 70
                and RUNNING_HEADER_RE.match(stripped)
                and not HEADING_RE.match(stripped)
                and not stripped.startswith(("SECTION", "SUBSECTION", "APPENDIX", "INTRODUCTION"))):
            continue
        seen_content = True
        cleaned.append(stripped)
    return "\n".join(cleaned)


def parse_toc_titles(pages):
    """Parse item titles from the table of contents pages."""
    titles = {}
    for i in range(3, 20):
        if i >= len(pages):
            break
        for line in pages[i].splitlines():
            m = TOC_LINE_RE.match(line.strip())
            if m and m.group(1) not in titles:
                titles[m.group(1)] = re.sub(r"\s+", " ", m.group(2)).strip()
    return titles


def title_from_item_page(cleaned_page):
    """Fallback: the item title is the text before the first 'Format' line."""
    parts = []
    for line in cleaned_page.splitlines():
        if line.strip() == "Format":
            break
        parts.append(line.strip())
        if len(parts) > 3:  # titles are 1-3 lines; bail if page layout differs
            return None
    title = re.sub(r"\s+", " ", " ".join(parts)).strip()
    return title or None


def slugify(section):
    return re.sub(r"[^a-z0-9]+", "-", section.lower()).strip("-")


def split_words(text, max_words):
    """Split text into chunks of at most max_words, breaking at paragraph
    (newline) boundaries where possible, else at sentence boundaries."""
    words = text.split()
    if len(words) <= max_words:
        return [text]

    parts = []
    current = []
    count = 0
    for paragraph in text.split("\n"):
        pwords = paragraph.split()
        if count + len(pwords) > max_words and count >= MIN_CHUNK_WORDS:
            parts.append("\n".join(current))
            current = []
            count = 0
        # A single paragraph longer than max_words: flush any pending text first
        # (preserving document order), then split at sentence boundaries,
        # hard-slicing any single sentence longer than max_words so the split
        # always makes progress.
        if len(pwords) > max_words:
            if current:
                parts.append("\n".join(current))
                current = []
                count = 0
            pieces = []
            for s in re.split(r"(?<=[.!?])\s+", paragraph):
                sw = s.split()
                while len(sw) > max_words:
                    pieces.append(" ".join(sw[:max_words]))
                    sw = sw[max_words:]
                if sw:
                    pieces.append(" ".join(sw))
            acc = []
            acc_count = 0
            for piece in pieces:
                pw = len(piece.split())
                if acc_count + pw > max_words and acc_count >= MIN_CHUNK_WORDS:
                    parts.append(" ".join(acc))
                    acc = []
                    acc_count = 0
                acc.append(piece)
                acc_count += pw
            paragraph = " ".join(acc)
            pwords = paragraph.split()
        current.append(paragraph)
        count += len(pwords)
    if current:
        parts.append("\n".join(current))
    parts = [p for p in parts if p.strip()]
    # Merge a small trailing part into its predecessor
    if len(parts) > 1 and len(parts[-1].split()) < MIN_CHUNK_WORDS:
        tail = parts.pop()
        parts[-1] = parts[-1] + "\n" + tail
    return parts


def add_chunks(chunks, section, title, text, used_ids):
    """Append one or more chunks for a block of text, splitting long blocks."""
    text = re.sub(r"\n{2,}", "\n", text).strip()
    if len(text.split()) < MIN_CHUNK_WORDS:
        return
    parts = split_words(text, MAX_CHUNK_WORDS)
    for idx, part in enumerate(parts):
        base = slugify(section)
        chunk_id = base if idx == 0 else f"{base}-p{idx + 1}"
        # Guarantee unique ids (a subsection heading can appear twice)
        suffix = 2
        while chunk_id in used_ids:
            chunk_id = f"{base}-{suffix}" if idx == 0 else f"{base}-{suffix}-p{idx + 1}"
            suffix += 1
        used_ids.add(chunk_id)
        part_title = title if idx == 0 else f"{title} (continued)"
        chunks.append({"id": chunk_id, "section": section, "title": part_title, "text": part})


def process_snbi(pdf_path, output_path):
    from pypdf import PdfReader

    reader = PdfReader(pdf_path)
    raw_pages = [p.extract_text() or "" for p in reader.pages]
    print(f"Extracted text from {len(raw_pages)} pages")

    toc_titles = parse_toc_titles(raw_pages)
    cleaned = [clean_page(t) for t in raw_pages]

    # Locate structural boundaries: page indexes where an item definition,
    # a section/subsection overview, the introduction, or an appendix begins.
    boundaries = []  # (page_index, kind, key)
    seen_items = set()
    intro_start = None
    example_start = None
    appendix_bounds = {}

    for i, text in enumerate(cleaned):
        first_line = text.splitlines()[0].strip() if text else ""
        if first_line == "INTRODUCTION" and intro_start is None:
            intro_start = i
            boundaries.append((i, "intro", "Introduction"))
            continue
        if first_line == "COMPREHENSIVE EXAMPLE" and example_start is None:
            example_start = i
            boundaries.append((i, "skip", "Comprehensive Example"))
            continue
        m = re.match(r"^APPENDIX ([A-C])\b", first_line)
        if m and m.group(1) not in appendix_bounds:
            appendix_bounds[m.group(1)] = i
            if m.group(1) == "C":
                boundaries.append((i, "appendix", "Appendix C"))
            else:
                boundaries.append((i, "skip", f"Appendix {m.group(1)}"))
            continue
        hm = HEADING_RE.match(first_line)
        if hm:
            number = hm.group(2) or hm.group(3)
            name = re.sub(r"\s+", " ", hm.group(4)).strip().title()
            boundaries.append((i, "overview", (f"Section {number}", name)))
            continue
        im = ITEM_ID_RE.search(text)
        if im and im.group(1) not in seen_items:
            seen_items.add(im.group(1))
            boundaries.append((i, "item", im.group(1)))

    boundaries.sort(key=lambda b: b[0])
    print(f"Found {sum(1 for b in boundaries if b[1] == 'item')} item definitions, "
          f"{sum(1 for b in boundaries if b[1] == 'overview')} overview headings")

    chunks = []
    used_ids = set()

    for bi, (start, kind, key) in enumerate(boundaries):
        end = boundaries[bi + 1][0] if bi + 1 < len(boundaries) else len(cleaned)
        if kind == "skip":
            continue
        block_text = "\n".join(cleaned[start:end])

        if kind == "intro":
            add_chunks(chunks, "Introduction", "SNBI Introduction", block_text, used_ids)
        elif kind == "appendix":
            add_chunks(chunks, "Appendix C", "Component Condition Rating Guidance",
                       block_text, used_ids)
        elif kind == "overview":
            section, name = key
            add_chunks(chunks, section, f"{name} (Overview)", block_text, used_ids)
        elif kind == "item":
            title = toc_titles.get(key) or title_from_item_page(cleaned[start]) or key
            prefix = key.rsplit(".", 1)[0]
            subsection = SUBSECTION_NAMES.get(prefix)
            full_title = f"{title} — Subsection {subsection}" if subsection else title
            add_chunks(chunks, key, full_title, block_text, used_ids)

    with open(output_path, "w", encoding="utf-8") as f:
        json.dump(chunks, f, separators=(",", ":"), ensure_ascii=False)

    total_words = sum(len(c["text"].split()) for c in chunks)
    item_sections = {c["section"] for c in chunks if re.match(r"^B\.", c["section"])}
    print(f"Wrote {len(chunks)} chunks ({total_words:,} words) covering "
          f"{len(item_sections)} data items")
    print(f"Output: {output_path}")
    print(f"File size: {os.path.getsize(output_path) / 1024:.0f} KB")
    word_counts = sorted(len(c["text"].split()) for c in chunks)
    print(f"Chunk words: min={word_counts[0]}, median={word_counts[len(word_counts) // 2]}, "
          f"max={word_counts[-1]}")


if __name__ == "__main__":
    script_dir = os.path.dirname(os.path.abspath(__file__))
    raw_dir = os.path.join(script_dir, "raw")
    pdf_file = os.path.join(raw_dir, "snbi.pdf")
    output_file = os.path.join(script_dir, "..", "src", "BridgeInsight",
                               "wwwroot", "data", "snbi-chunks.json")

    os.makedirs(raw_dir, exist_ok=True)
    if not os.path.exists(pdf_file):
        download_pdf(pdf_file)
    else:
        print(f"Using cached PDF: {pdf_file}")

    process_snbi(pdf_file, output_file)
