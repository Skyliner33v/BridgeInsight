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

## Data Source

Bridge data sourced from the [FHWA National Bridge Inventory](https://www.fhwa.dot.gov/bridge/nbi/ascii.cfm) (2024 submission, Washington State). Contains 8,474 bridges with condition ratings, structural data, traffic volumes, and inspection records.

All condition ratings follow the FHWA Recording and Coding Guide for the Structure Inventory and Appraisal of the Nation's Bridges (0-9 scale).

## License

MIT
