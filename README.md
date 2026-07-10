# BridgeInsight

**Explainable AI for Bridge Data Intelligence**

A Blazor WebAssembly application that transforms National Bridge Inventory (NBI) data into trustworthy, explainable analysis for bridge program managers and civil engineering decision-makers.

**[Live Demo](https://Skyliner33v.github.io/BridgeInsight/)**

## Features

### Bridge Explorer
- Search, filter, and sort 8,474 Washington State bridges
- Filter by county, condition rating, year built, traffic volume, structural deficiency, and scour vulnerability
- Color-coded condition ratings for instant visual assessment
- Paginated data grid with summary statistics

### AI-Powered Bridge Analysis
- Claude API integration generates plain-English condition narratives
- **Evidence chain traceability** — every AI claim traces to specific NBI data fields and FHWA standards
- Risk factor identification with severity classification
- Data gap flagging and recommended actions
- Demo mode with pre-cached responses (no API key required)

### Portfolio Risk Briefing
- Multi-bridge AI briefing for non-technical decision-makers
- Risk tier classification (Immediate / Near-Term / Monitor / Satisfactory)
- Comparative analysis across bridge portfolios
- Funding prioritization narratives
- Data quality notes and caveats

### Ask the SNBI Guide
- Document-grounded Q&A over the FHWA Specifications for the National Bridge Inventory (SNBI, Publication No. FHWA-HIF-22-017)
- Fully client-side retrieval — lexical scoring ranks spec sections in the browser, no embeddings or server
- **Mandatory citations** — the AI answers only from retrieved sections, cites the section number for every claim, and declines questions the sections don't cover
- Transparency panel shows exactly which sections were retrieved and sent to the model
- Citation chips expand to the verbatim quote and full source excerpt
- Demo mode with three pre-cached, citation-grounded answers (no API key required)

### Portfolio Site
- Professional portfolio showcasing bridge engineering and software development experience
- Projects, skills, and contact information

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Blazor WebAssembly (.NET 8) |
| Database | SQLite via EF Core (in-browser) |
| Browser SQLite | BlazorWASMEntityFrameworkSQLite |
| AI | Claude API (Sonnet 5) with RAG context |
| Deployment | GitHub Pages via GitHub Actions |
| Data | FHWA National Bridge Inventory (2024) |

## Architecture

```
Browser (WASM)
├── Blazor Components (Razor)
├── Entity Framework Core
│   └── SQLite (Emscripten filesystem + Cache API)
├── Claude API Client (direct browser access)
│   └── RAG: FHWA rating definitions in system prompt
├── Ask the SNBI Guide (document-grounded Q&A)
│   └── Client-side lexical retrieval over pre-extracted SNBI chunks
└── Demo Mode (pre-cached JSON responses)
```

**Key architectural decisions:**
- **Client-side only** — no server required, runs entirely in the browser
- **Offline-capable** — SQLite database persists via browser Cache API
- **Explainable AI** — structured JSON output with evidence chains, not black-box summaries
- **Demo-first** — works without an API key using pre-cached responses

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- wasm-tools workload: `dotnet workload install wasm-tools`

### Run Locally
```bash
dotnet run --project src/BridgeInsight
```
Navigate to `https://localhost:5001` (or the port shown in console output).

### Using AI Analysis
1. Navigate to any bridge detail page
2. Click "Settings" and enter your [Anthropic API key](https://console.anthropic.com/)
3. Click "Generate AI Analysis"
4. Without an API key, demo mode provides pre-cached responses for select bridges

### Data Preprocessing
To regenerate bridge data from source:
```bash
cd tools
python preprocess_nbi.py
```
Downloads WA 2024 NBI data from FHWA and outputs `wa-bridges-2024.json`.

To regenerate the SNBI specification chunks for Ask the SNBI Guide:
```bash
cd tools
pip install --user pypdf
python preprocess_snbi.py
```
Downloads the official SNBI PDF from FHWA and extracts section-level chunks into `snbi-chunks.json`. The generated JSON is committed so the deployed app never fetches FHWA at runtime.

## Data Sources

Bridge data sourced from the [FHWA National Bridge Inventory](https://www.fhwa.dot.gov/bridge/nbi/ascii.cfm) (2024 submission, Washington State). Contains 8,474 bridges with condition ratings, structural data, traffic volumes, and inspection records.

All condition ratings follow the FHWA Recording and Coding Guide for the Structure Inventory and Appraisal of the Nation's Bridges (0-9 scale).

Ask the SNBI Guide is grounded in the [FHWA Specifications for the National Bridge Inventory](https://www.fhwa.dot.gov/bridge/snbi/errata1_to_snbi_march_2022_publication.pdf) (March 2022 with errata #1). The extracted corpus covers the introduction, all 154 data item definitions, section overviews (including the component condition rating code tables), and Appendix C condition rating guidance. The comprehensive example walkthrough and Appendixes A–B (example data sets and indexes) are intentionally excluded, and figures and some multi-column tables are flattened by PDF text extraction — verify answers against the official publication.

## License

MIT
