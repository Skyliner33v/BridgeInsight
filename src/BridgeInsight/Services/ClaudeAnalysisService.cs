using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using BridgeInsight.Models;
using BridgeInsight.Reference;
using Microsoft.JSInterop;

namespace BridgeInsight.Services;

public class ClaudeAnalysisService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public ClaudeAnalysisService(HttpClient http, IJSRuntime js)
    {
        _http = http;
        _js = js;
    }

    public async Task<string?> GetApiKeyAsync()
        => await _js.InvokeAsync<string?>("localStorage.getItem", "bridgeinsight_api_key");

    public async Task SetApiKeyAsync(string key)
        => await _js.InvokeVoidAsync("localStorage.setItem", "bridgeinsight_api_key", key);

    public async Task ClearApiKeyAsync()
        => await _js.InvokeVoidAsync("localStorage.removeItem", "bridgeinsight_api_key");

    public async Task<bool> HasApiKeyAsync()
        => !string.IsNullOrEmpty(await GetApiKeyAsync());

    public async Task<BridgeAnalysis> AnalyzeBridgeAsync(Bridge bridge)
    {
        var apiKey = await GetApiKeyAsync();

        if (string.IsNullOrEmpty(apiKey))
            return await GetDemoAnalysisAsync(bridge.StructureNumber);

        try
        {
            var systemPrompt = BuildSystemPrompt();
            var userPrompt = BuildBridgePrompt(bridge);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages");
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");
            request.Headers.Add("anthropic-dangerous-direct-browser-access", "true");

            var body = new
            {
                model = "claude-sonnet-4-20250514",
                max_tokens = 4096,
                temperature = 0.3,
                system = systemPrompt,
                messages = new[] { new { role = "user", content = userPrompt } }
            };

            request.Content = JsonContent.Create(body);
            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return new BridgeAnalysis
                {
                    Error = $"API Error ({response.StatusCode}): {error}",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return ParseAnalysisResponse(responseJson);
        }
        catch (Exception ex)
        {
            return new BridgeAnalysis
            {
                Error = $"Error: {ex.Message}",
                GeneratedAt = DateTime.UtcNow
            };
        }
    }

    public async Task<PortfolioBriefingResult> GeneratePortfolioBriefingAsync(List<Bridge> bridges)
    {
        var apiKey = await GetApiKeyAsync();

        if (string.IsNullOrEmpty(apiKey))
            return await GetDemoPortfolioBriefingAsync();

        try
        {
            var systemPrompt = BuildPortfolioSystemPrompt();
            var userPrompt = BuildPortfolioPrompt(bridges);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages");
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");
            request.Headers.Add("anthropic-dangerous-direct-browser-access", "true");

            var body = new
            {
                model = "claude-sonnet-4-20250514",
                max_tokens = 8192,
                temperature = 0.3,
                system = systemPrompt,
                messages = new[] { new { role = "user", content = userPrompt } }
            };

            request.Content = JsonContent.Create(body);
            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return new PortfolioBriefingResult
                {
                    Error = $"API Error ({response.StatusCode}): {error}",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return ParsePortfolioResponse(responseJson);
        }
        catch (Exception ex)
        {
            return new PortfolioBriefingResult
            {
                Error = $"Error: {ex.Message}",
                GeneratedAt = DateTime.UtcNow
            };
        }
    }

    private string BuildSystemPrompt()
    {
        return $@"You are BridgeInsight, an AI assistant specialized in analyzing National Bridge Inventory (NBI) data for bridge program managers and civil engineering decision-makers.

Your role is to translate raw NBI data into clear, defensible, plain-English assessments. Every claim you make must be traceable to specific data fields. You never speculate beyond what the data supports.

You are NOT making engineering judgments about structural capacity or safety. You are interpreting standardized NBI condition codes and identifying patterns that warrant attention from qualified engineers.

{FhwaRatings.GetFullReferenceText()}

Output your analysis as valid JSON with this structure:
{{
  ""summary"": ""Plain-English condition narrative (2-3 paragraphs)"",
  ""evidence_chain"": [
    {{
      ""claim"": ""The claim made in the summary"",
      ""data_fields"": [""Item 59: Substructure = 4""],
      ""standard_reference"": ""Per FHWA NBI Coding Guide, a rating of 4 indicates..."",
      ""reasoning"": ""This rating combined with...""
    }}
  ],
  ""risk_factors"": [
    {{
      ""factor"": ""Name of risk factor"",
      ""severity"": ""high|medium|low"",
      ""detail"": ""Explanation""
    }}
  ],
  ""data_gaps"": [""List of missing or stale data""],
  ""recommended_actions"": [
    {{
      ""priority"": 1,
      ""action"": ""Description"",
      ""rationale"": ""Why""
    }}
  ]
}}

Return ONLY the JSON object, no markdown code fences or other text.";
    }

    private string BuildBridgePrompt(Bridge bridge)
    {
        return $@"Analyze this bridge from the National Bridge Inventory:

BRIDGE IDENTIFICATION:
- Structure Number: {bridge.StructureNumber}
- Facility Carried: {bridge.FacilityCarried}
- Features Intersected: {bridge.FeaturesIntersected}
- County: {bridge.CountyName} ({bridge.CountyCode})
- Location: {bridge.Latitude:F6}, {bridge.Longitude:F6}

STRUCTURE:
- Main Span Material (Item 43A): {bridge.MainSpanMaterial} — {StructureTypes.GetMaterial(bridge.MainSpanMaterial)}
- Main Span Design (Item 43B): {bridge.MainSpanDesign} — {StructureTypes.GetDesign(bridge.MainSpanDesign)}
- Year Built (Item 27): {bridge.YearBuilt?.ToString() ?? "N/A"} (Age: {(bridge.YearBuilt.HasValue ? bridge.Age.ToString() + " years" : "N/A")})
- Year Reconstructed (Item 106): {bridge.YearReconstructed?.ToString() ?? "N/A"}
- Structure Length (Item 49): {bridge.StructureLength?.ToString("F1") ?? "N/A"} ft
- Bridge Roadway Width (Item 51): {bridge.BridgeRoadwayWidth?.ToString("F1") ?? "N/A"} ft

CONDITION RATINGS (0-9 scale):
- Deck Condition (Item 58): {FormatRating(bridge.DeckCondition)}
- Superstructure Condition (Item 59): {FormatRating(bridge.SuperstructureCondition)}
- Substructure Condition (Item 60): {FormatRating(bridge.SubstructureCondition)}
- Culvert Condition (Item 62): {FormatRating(bridge.CulvertCondition)}
- Channel Condition (Item 61): {FormatRating(bridge.ChannelCondition)}
- Waterway Adequacy (Item 71): {FormatRating(bridge.WaterwayAdequacy)}

TRAFFIC:
- Average Daily Traffic (Item 29): {bridge.AverageDailyTraffic?.ToString("N0") ?? "N/A"}
- Truck Traffic (Item 109): {bridge.TruckTrafficPercent?.ToString() ?? "N/A"}%

APPRAISAL:
- Structural Evaluation (Item 67): {bridge.StructuralEvaluation}
- Deck Geometry Evaluation (Item 68): {bridge.DeckGeometryEvaluation}

STATUS:
- Open/Posted/Closed (Item 41): {bridge.OpenPostedClosed}
- Scour Critical (Item 113): {bridge.ScourCritical}
- Bridge Posting (Item 70): {bridge.BridgePosting?.ToString() ?? "N/A"}

OWNERSHIP:
- Owner (Item 22): {bridge.Owner} — {OwnerCodes.GetOwner(bridge.Owner)}
- Maintenance (Item 21): {bridge.MaintenanceResponsibility} — {OwnerCodes.GetMaintenanceResponsibility(bridge.MaintenanceResponsibility)}

INSPECTION:
- Last Inspection (Item 90): {bridge.InspectionDate?.ToString("MM/yyyy") ?? "N/A"}
- Inspection Frequency (Item 91): {bridge.InspectionFrequency?.ToString() ?? "N/A"} months

Structurally Deficient: {(bridge.IsStructurallyDeficient ? "YES" : "No")}

Provide a thorough analysis following the JSON format specified.";
    }

    private string BuildPortfolioSystemPrompt()
    {
        return $@"You are BridgeInsight, generating a portfolio risk briefing for bridge program managers. This document should be suitable for presenting to non-technical decision-makers such as county commissioners or legislative committees.

You are analyzing a set of bridges and must:
1. Classify each bridge into risk tiers with clear, traceable criteria
2. Identify patterns across the portfolio
3. Provide a funding prioritization narrative in plain English
4. Flag data quality issues

Every statement must be traceable to specific NBI data. Never speculate beyond the data.

{FhwaRatings.GetFullReferenceText()}

Output as valid JSON:
{{
  ""executive_summary"": ""..."",
  ""risk_tiers"": {{
    ""immediate_attention"": {{ ""bridges"": [...], ""rationale"": ""..."" }},
    ""near_term_priority"": {{ ""bridges"": [...], ""rationale"": ""..."" }},
    ""monitor"": {{ ""bridges"": [...], ""rationale"": ""..."" }},
    ""satisfactory"": {{ ""bridges"": [...], ""rationale"": ""..."" }}
  }},
  ""comparative_analysis"": {{
    ""patterns"": [""...""],
    ""prevalent_risk_factors"": [""...""],
    ""data_gaps"": [""...""]
  }},
  ""bridge_evidence_blocks"": [
    {{
      ""structure_number"": ""..."",
      ""facility"": ""..."",
      ""key_ratings"": {{ ""deck"": 5, ""superstructure"": 6, ""substructure"": 4 }},
      ""one_sentence_assessment"": ""..."",
      ""risk_factors"": [""...""],
      ""action_category"": ""...""
    }}
  ],
  ""funding_narrative"": ""..."",
  ""data_quality_notes"": [""...""]
}}

Return ONLY the JSON object, no markdown code fences or other text.";
    }

    private string BuildPortfolioPrompt(List<Bridge> bridges)
    {
        var bridgeData = string.Join("\n\n", bridges.Select(b => $@"Bridge: {b.StructureNumber} — {b.FacilityCarried} over {b.FeaturesIntersected}
County: {b.CountyName} | Built: {b.YearBuilt?.ToString() ?? "N/A"} | ADT: {b.AverageDailyTraffic?.ToString("N0") ?? "N/A"}
Deck: {FormatRating(b.DeckCondition)} | Super: {FormatRating(b.SuperstructureCondition)} | Sub: {FormatRating(b.SubstructureCondition)} | Culvert: {FormatRating(b.CulvertCondition)}
Scour: {b.ScourCritical} | Owner: {OwnerCodes.GetOwner(b.Owner)}
SD: {(b.IsStructurallyDeficient ? "YES" : "No")} | Type: {StructureTypes.GetMaterial(b.MainSpanMaterial)} {StructureTypes.GetDesign(b.MainSpanDesign)}"));

        return $@"Generate a portfolio risk briefing for the following {bridges.Count} bridges:

{bridgeData}

Provide a comprehensive briefing following the JSON format specified.";
    }

    private static string FormatRating(int? rating)
    {
        if (rating == null) return "N/A";
        return $"{rating} — {FhwaRatings.GetRatingLabel(rating)}";
    }

    private BridgeAnalysis ParseAnalysisResponse(string responseJson)
    {
        try
        {
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            // Extract text content from Claude's response
            var content = root.GetProperty("content")[0].GetProperty("text").GetString();
            if (string.IsNullOrEmpty(content))
                return new BridgeAnalysis { Error = "Empty response from API" };

            // Clean any markdown code fences
            content = content.Trim();
            if (content.StartsWith("```"))
            {
                var firstNewline = content.IndexOf('\n');
                content = content.Substring(firstNewline + 1);
                if (content.EndsWith("```"))
                    content = content.Substring(0, content.Length - 3);
                content = content.Trim();
            }

            var analysis = JsonSerializer.Deserialize<BridgeAnalysis>(content, JsonOptions);
            if (analysis == null)
                return new BridgeAnalysis { Error = "Failed to parse analysis response" };

            analysis.GeneratedAt = DateTime.UtcNow;
            return analysis;
        }
        catch (Exception ex)
        {
            return new BridgeAnalysis { Error = $"Parse error: {ex.Message}", GeneratedAt = DateTime.UtcNow };
        }
    }

    private PortfolioBriefingResult ParsePortfolioResponse(string responseJson)
    {
        try
        {
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;
            var content = root.GetProperty("content")[0].GetProperty("text").GetString();

            if (string.IsNullOrEmpty(content))
                return new PortfolioBriefingResult { Error = "Empty response from API" };

            content = content.Trim();
            if (content.StartsWith("```"))
            {
                var firstNewline = content.IndexOf('\n');
                content = content.Substring(firstNewline + 1);
                if (content.EndsWith("```"))
                    content = content.Substring(0, content.Length - 3);
                content = content.Trim();
            }

            var briefing = JsonSerializer.Deserialize<PortfolioBriefingResult>(content, JsonOptions);
            if (briefing == null)
                return new PortfolioBriefingResult { Error = "Failed to parse briefing response" };

            briefing.GeneratedAt = DateTime.UtcNow;
            return briefing;
        }
        catch (Exception ex)
        {
            return new PortfolioBriefingResult { Error = $"Parse error: {ex.Message}", GeneratedAt = DateTime.UtcNow };
        }
    }

    private async Task<BridgeAnalysis> GetDemoAnalysisAsync(string structureNumber)
    {
        try
        {
            // Try to load a specific demo response
            var demoFiles = new[] { "bridge-demo-1.json", "bridge-demo-2.json", "bridge-demo-3.json" };
            foreach (var file in demoFiles)
            {
                try
                {
                    var demo = await _http.GetFromJsonAsync<DemoResponse>($"data/demo-responses/{file}", JsonOptions);
                    if (demo?.StructureNumber == structureNumber)
                    {
                        var analysis = demo.Analysis;
                        analysis.IsDemo = true;
                        return analysis;
                    }
                }
                catch { }
            }

            // Return a generic demo response if no specific match
            var genericDemo = await _http.GetFromJsonAsync<DemoResponse>("data/demo-responses/bridge-demo-1.json", JsonOptions);
            if (genericDemo != null)
            {
                genericDemo.Analysis.IsDemo = true;
                return genericDemo.Analysis;
            }
        }
        catch { }

        return new BridgeAnalysis
        {
            Summary = "Demo mode: No pre-cached analysis available for this bridge. Enter a Claude API key in Settings to generate live analysis for any bridge.",
            IsDemo = true,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private async Task<PortfolioBriefingResult> GetDemoPortfolioBriefingAsync()
    {
        try
        {
            var demo = await _http.GetFromJsonAsync<PortfolioBriefingResult>(
                "data/demo-responses/portfolio-briefing.json", JsonOptions);
            if (demo != null)
            {
                demo.IsDemo = true;
                return demo;
            }
        }
        catch { }

        return new PortfolioBriefingResult
        {
            ExecutiveSummary = "Demo mode: Enter a Claude API key in Settings to generate live portfolio briefings.",
            IsDemo = true,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private class DemoResponse
    {
        public string StructureNumber { get; set; } = "";
        public BridgeAnalysis Analysis { get; set; } = new();
    }
}
