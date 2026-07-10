using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
    {
        try
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", "bridgeinsight_api_key");
        }
        catch
        {
            // localStorage can be blocked (sandboxed iframe, strict privacy
            // settings) — treat it as "no key" so the app falls back to demo mode
            return null;
        }
    }

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
                model = "claude-sonnet-5",
                max_tokens = 4096,
                thinking = new { type = "disabled" },
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
                model = "claude-sonnet-5",
                max_tokens = 8192,
                thinking = new { type = "disabled" },
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

    public async Task<SnbiAnswerResult> AskSnbiAsync(string question, List<SnbiChunk> retrievedChunks)
    {
        var apiKey = await GetApiKeyAsync();

        if (string.IsNullOrEmpty(apiKey))
            return await GetDemoSnbiAnswerAsync(question);

        try
        {
            var systemPrompt = BuildSnbiSystemPrompt();
            var userPrompt = BuildSnbiPrompt(question, retrievedChunks);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages");
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");
            request.Headers.Add("anthropic-dangerous-direct-browser-access", "true");

            var body = new
            {
                model = "claude-sonnet-5",
                max_tokens = 4096,
                thinking = new { type = "disabled" },
                system = systemPrompt,
                messages = new[] { new { role = "user", content = userPrompt } }
            };

            request.Content = JsonContent.Create(body);
            var response = await _http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return new SnbiAnswerResult
                {
                    Error = $"API Error ({response.StatusCode}): {error}",
                    GeneratedAt = DateTime.UtcNow
                };
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            return ParseSnbiResponse(responseJson);
        }
        catch (Exception ex)
        {
            return new SnbiAnswerResult
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

    private string BuildSnbiSystemPrompt()
    {
        return @"You are ""Ask the SNBI Guide"" for BridgeInsight — a document-grounded reference assistant for the FHWA Specifications for the National Bridge Inventory (SNBI), Publication No. FHWA-HIF-22-017 (March 2022 with errata #1).

You will be given a question and a set of sections extracted from the SNBI. Follow these strict grounding rules:
1. Answer ONLY from the provided sections. Do not use any outside knowledge, even if you are confident you know the answer.
2. Cite the section identifier (e.g., B.C.01, Section 7.1, Appendix C, Introduction) for every claim you make. Include identifiers inline in the answer text where each claim is made.
3. Support each claim with a citation containing a short verbatim quote copied exactly from the provided section text.
4. If the provided sections do not contain the information needed to answer the question, set the answer to exactly ""Not covered in the provided sections."" with an empty citations list.
5. Never speculate, extrapolate, or generalize beyond what the quoted text supports.

Output your answer as valid JSON with this structure:
{
  ""answer"": ""Plain-English answer with inline section identifiers (1-3 paragraphs)"",
  ""citations"": [
    {
      ""section"": ""B.C.01"",
      ""quote"": ""short verbatim quote from that section supporting the claim""
    }
  ]
}

Return ONLY the JSON object, no markdown code fences or other text.";
    }

    private string BuildSnbiPrompt(string question, List<SnbiChunk> retrievedChunks)
    {
        var sections = string.Join("\n\n", retrievedChunks.Select(c =>
            $"=== [{c.Section}] {c.Title} ===\n{c.Text}"));

        return $@"QUESTION: {question}

PROVIDED SNBI SECTIONS:

{sections}

Answer the question following the JSON format specified, grounded only in the sections above.";
    }

    private static string FormatRating(int? rating)
    {
        if (rating == null) return "N/A";
        return $"{rating} — {FhwaRatings.GetRatingLabel(rating)}";
    }

    // Matches a fenced JSON object anywhere in the text, tolerating a missing
    // newline after the opening fence and prose before/after the fences
    private static readonly Regex FencedJsonRegex =
        new(@"```(?:json)?\s*(\{.*\})\s*```", RegexOptions.Singleline | RegexOptions.Compiled);

    /// <summary>
    /// Returns a user-facing error when the Messages API response ended abnormally
    /// (truncated at max_tokens, or refused by the model), otherwise null.
    /// </summary>
    private static string? GetStopReasonError(JsonElement root)
    {
        if (!root.TryGetProperty("stop_reason", out var stopReason) ||
            stopReason.ValueKind != JsonValueKind.String)
        {
            return null;
        }

        return stopReason.GetString() switch
        {
            "max_tokens" => "The response was cut off before completing (max_tokens reached). Try again, or reduce the input size.",
            "refusal" => "The model declined to generate a response for this request.",
            _ => null
        };
    }

    /// <summary>
    /// Extracts the JSON object payload from a model text response, tolerating
    /// markdown code fences and leading/trailing prose. Returns null when the
    /// text contains no JSON object. Internal for unit testing.
    /// </summary>
    internal static string? ExtractJsonPayload(string content)
    {
        var fenced = FencedJsonRegex.Match(content);
        if (fenced.Success)
            return fenced.Groups[1].Value;

        var start = content.IndexOf('{');
        var end = content.LastIndexOf('}');
        if (start >= 0 && end > start)
            return content.Substring(start, end - start + 1);

        return null;
    }

    private static string Truncate(string text, int maxLength)
        => text.Length <= maxLength ? text : text[..maxLength] + "…";

    /// <summary>
    /// Extracts the text from the first content block of type "text" in a Messages API
    /// response. Robust to non-text blocks (e.g. thinking) appearing before the text block.
    /// </summary>
    private static string? ExtractTextContent(JsonElement root)
    {
        if (!root.TryGetProperty("content", out var content) || content.ValueKind != JsonValueKind.Array)
            return null;

        foreach (var block in content.EnumerateArray())
        {
            if (block.TryGetProperty("type", out var type) &&
                type.GetString() == "text" &&
                block.TryGetProperty("text", out var text))
            {
                return text.GetString();
            }
        }

        return null;
    }

    private BridgeAnalysis ParseAnalysisResponse(string responseJson)
    {
        try
        {
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            var stopError = GetStopReasonError(root);
            if (stopError != null)
                return new BridgeAnalysis { Error = stopError, GeneratedAt = DateTime.UtcNow };

            // Extract text content from Claude's response (first block where type == "text")
            var content = ExtractTextContent(root);
            if (string.IsNullOrEmpty(content))
                return new BridgeAnalysis { Error = "Empty response from API" };

            var json = ExtractJsonPayload(content.Trim());
            if (json == null)
                return new BridgeAnalysis
                {
                    Error = $"The model returned an unexpected response: {Truncate(content.Trim(), 300)}",
                    GeneratedAt = DateTime.UtcNow
                };

            var analysis = JsonSerializer.Deserialize<BridgeAnalysis>(json, JsonOptions);
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

            var stopError = GetStopReasonError(root);
            if (stopError != null)
                return new PortfolioBriefingResult { Error = stopError, GeneratedAt = DateTime.UtcNow };

            var content = ExtractTextContent(root);
            if (string.IsNullOrEmpty(content))
                return new PortfolioBriefingResult { Error = "Empty response from API" };

            var json = ExtractJsonPayload(content.Trim());
            if (json == null)
                return new PortfolioBriefingResult
                {
                    Error = $"The model returned an unexpected response: {Truncate(content.Trim(), 300)}",
                    GeneratedAt = DateTime.UtcNow
                };

            var briefing = JsonSerializer.Deserialize<PortfolioBriefingResult>(json, JsonOptions);
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

    // Static (uses no instance state) and internal for unit testing
    internal static SnbiAnswerResult ParseSnbiResponse(string responseJson)
    {
        try
        {
            using var doc = JsonDocument.Parse(responseJson);
            var root = doc.RootElement;

            var stopError = GetStopReasonError(root);
            if (stopError != null)
                return new SnbiAnswerResult { Error = stopError, GeneratedAt = DateTime.UtcNow };

            var content = ExtractTextContent(root);
            if (string.IsNullOrEmpty(content))
                return new SnbiAnswerResult { Error = "Empty response from API" };

            var json = ExtractJsonPayload(content.Trim());
            if (json == null)
                return new SnbiAnswerResult
                {
                    Error = $"The model returned an unexpected response: {Truncate(content.Trim(), 300)}",
                    GeneratedAt = DateTime.UtcNow
                };

            var answer = JsonSerializer.Deserialize<SnbiAnswerResult>(json, JsonOptions);
            if (answer == null)
                return new SnbiAnswerResult { Error = "Failed to parse answer response" };

            // The model can emit explicit nulls despite the prompt's schema —
            // normalize so the UI never renders against null members
            answer.Answer ??= "";
            answer.Citations ??= new List<SnbiCitation>();
            answer.GeneratedAt = DateTime.UtcNow;
            return answer;
        }
        catch (Exception ex)
        {
            return new SnbiAnswerResult { Error = $"Parse error: {ex.Message}", GeneratedAt = DateTime.UtcNow };
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

    private async Task<SnbiAnswerResult> GetDemoSnbiAnswerAsync(string question)
    {
        var normalizedQuestion = NormalizeQuestion(question);

        try
        {
            var demoFiles = new[] { "snbi-demo-1.json", "snbi-demo-2.json", "snbi-demo-3.json" };
            foreach (var file in demoFiles)
            {
                try
                {
                    var demo = await _http.GetFromJsonAsync<SnbiDemoResponse>($"data/demo-responses/{file}", JsonOptions);
                    if (demo != null && NormalizeQuestion(demo.Question) == normalizedQuestion)
                    {
                        var answer = demo.Answer;
                        answer.IsDemo = true;
                        return answer;
                    }
                }
                catch { }
            }
        }
        catch { }

        return new SnbiAnswerResult
        {
            Answer = "Demo mode: pre-cached answers are available for the three sample questions shown on this page. Enter a Claude API key on the BridgeInsight hub to ask any question about the SNBI specification.",
            IsDemo = true,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private static string NormalizeQuestion(string question)
        => new string(question.ToLowerInvariant().Where(char.IsLetterOrDigit).ToArray());

    private class DemoResponse
    {
        public string StructureNumber { get; set; } = "";
        public BridgeAnalysis Analysis { get; set; } = new();
    }

    private class SnbiDemoResponse
    {
        public string Question { get; set; } = "";
        public SnbiAnswerResult Answer { get; set; } = new();
    }
}
