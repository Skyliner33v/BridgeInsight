# Design Decisions

## Why BridgeInsight?

Bridge program managers routinely make multi-million-dollar decisions about infrastructure that keeps communities connected. They rely on National Bridge Inventory data — thousands of numeric codes that require specialized knowledge to interpret. The gap between raw data and actionable understanding is where errors, delays, and miscommunication happen.

BridgeInsight exists to close that gap by demonstrating that AI can translate bridge data into trustworthy, explainable narratives without replacing engineering judgment.

## Product Questions I Asked Before Writing Code

### Who is the user?
Bridge program managers, asset management engineers, and non-technical decision-makers (county commissioners, legislative staff) who need to understand bridge condition data without being NBI coding experts.

### What problem does this solve?
NBI data is a wall of numbers. A deck rating of "3" means nothing to a county commissioner but "serious deterioration with possible local failures" changes how they prioritize funding. BridgeInsight makes that translation automatic, traceable, and consistent.

### Why explainable AI instead of just AI?
Bridge engineering is a domain where trust is earned through transparency. An AI that says "this bridge is in poor condition" is useless if the engineer can't verify the reasoning. Every BridgeInsight claim traces back to a specific NBI field and FHWA standard — the engineer can check the AI's work in seconds.

### Why Blazor WASM instead of a traditional web app?
Three reasons:
1. **Offline capability** — Bridge inspectors work in the field where connectivity is unreliable. A WASM app with SQLite persistence works without a server.
2. **No backend to maintain** — Deployed to GitHub Pages as static files. No server costs, no infrastructure to manage.
3. **Domain alignment** — WSDOT and many DOTs use .NET ecosystems. Blazor demonstrates fluency in the technology stack these organizations use.

### Why SQLite in the browser?
Querying 8,474 bridges with complex filters (county + condition range + ADT + scour + text search) requires a real database engine. SQLite via BlazorWASMEntityFrameworkSQLite gives us full EF Core LINQ queries running in the browser with data persisted across sessions via the Cache API.

The alternative — loading all data into memory and filtering with LINQ-to-Objects — would work but wouldn't demonstrate the EF Core + SQLite pattern that's directly applicable to enterprise bridge management systems.

### Why direct browser API calls instead of a backend proxy?
Simplicity and transparency. The Anthropic API supports direct browser access via the `anthropic-dangerous-direct-browser-access` header. This eliminates the need for a backend server while keeping the API key in the user's control (stored in localStorage, never transmitted to any server I control).

For a production system, a backend proxy with rate limiting and key management would be appropriate. For a portfolio demonstration, direct access shows the pattern without unnecessary infrastructure.

## Technical Tradeoffs

### Data Size vs. Completeness
The preprocessed JSON file is ~7 MB for 8,474 bridges. I chose to include all Washington State bridges rather than a subset because:
- The full dataset demonstrates real-world data volume handling
- Subset selection would require justifying which bridges to include/exclude
- Gzip compression reduces transfer size significantly
- SQLite handles the full dataset without performance issues

### AI Model Selection
Claude Sonnet 4 was chosen over Opus for the right balance of quality, speed, and cost. The analysis tasks are well-defined with structured output requirements — they don't require the additional reasoning depth of Opus. Temperature of 0.3 provides consistent, conservative output appropriate for infrastructure assessment.

### Demo Mode Design
Pre-cached demo responses serve two purposes:
1. **Zero-friction evaluation** — Reviewers can see the full AI analysis capability without needing an API key
2. **Consistent demonstration** — The demo responses showcase the evidence chain pattern with carefully selected bridges (I-5, Hood Canal Bridge, Wynoochee River)

### CSS Architecture
Custom design tokens and utility classes instead of a CSS framework (Bootstrap, Tailwind). This demonstrates:
- Understanding of CSS custom properties and systematic design
- Condition-specific color system aligned with FHWA visual conventions
- Component-scoped styles via Blazor CSS isolation
- Responsive design without framework overhead

## What I'd Build Next

1. **Deterioration Trend Analysis** — With multiple years of NBI data, predict condition rating trajectories and estimate remaining service life
2. **Map Visualization** — Leaflet/Mapbox integration showing bridge locations with condition-colored markers
3. **Comparative State Analysis** — Extend beyond WA to compare bridge portfolios across states
4. **Element-Level Integration** — Incorporate BrM element-level inspection data for more granular analysis
5. **Export & Reporting** — PDF generation for portfolio briefings suitable for board presentations
6. **Multi-User Collaboration** — Shared bridge lists and annotations for team-based asset management
