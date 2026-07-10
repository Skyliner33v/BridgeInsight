using System.Text.Json;
using BridgeInsight.Services;
using Xunit;

namespace BridgeInsight.Tests;

public class ClaudeAnalysisServiceTests
{
    // ── ExtractJsonPayload ──────────────────────────────────────────────

    [Fact]
    public void ExtractJsonPayload_BareJsonObject_ReturnsItUnchanged()
    {
        var payload = ClaudeAnalysisService.ExtractJsonPayload("""{"answer":"x","citations":[]}""");

        Assert.Equal("""{"answer":"x","citations":[]}""", payload);
    }

    [Fact]
    public void ExtractJsonPayload_FencedJsonWithLanguageTag_ReturnsInnerObject()
    {
        var content = "```json\n{\"answer\": \"x\"}\n```";

        var payload = ClaudeAnalysisService.ExtractJsonPayload(content);

        Assert.Equal("{\"answer\": \"x\"}", payload);
    }

    [Fact]
    public void ExtractJsonPayload_FencedJsonWithoutLanguageTag_ReturnsInnerObject()
    {
        var content = "```\n{\"answer\": \"x\"}\n```";

        var payload = ClaudeAnalysisService.ExtractJsonPayload(content);

        Assert.Equal("{\"answer\": \"x\"}", payload);
    }

    [Fact]
    public void ExtractJsonPayload_FenceWithProseAroundIt_ReturnsInnerObject()
    {
        var content = "Here is the answer you asked for:\n```json\n{\"answer\": \"x\"}\n```\nLet me know if you need more.";

        var payload = ClaudeAnalysisService.ExtractJsonPayload(content);

        Assert.Equal("{\"answer\": \"x\"}", payload);
    }

    [Fact]
    public void ExtractJsonPayload_LeadingProseBeforeBareJson_ReturnsObject()
    {
        var content = "Sure! Here is the JSON: {\"answer\": \"x\", \"citations\": []}";

        var payload = ClaudeAnalysisService.ExtractJsonPayload(content);

        Assert.Equal("{\"answer\": \"x\", \"citations\": []}", payload);
    }

    [Fact]
    public void ExtractJsonPayload_PlainTextRefusal_ReturnsNull()
    {
        var content = "I can't help with that request.";

        var payload = ClaudeAnalysisService.ExtractJsonPayload(content);

        Assert.Null(payload);
    }

    [Fact]
    public void ExtractJsonPayload_NestedObjects_SpansFirstToLastBrace()
    {
        var content = """{"outer": {"inner": 1}, "list": [{"a": 2}]}""";

        var payload = ClaudeAnalysisService.ExtractJsonPayload(content);

        Assert.Equal(content, payload);
        // The extracted payload must stay parseable
        using var doc = JsonDocument.Parse(payload!);
    }

    // ── ParseSnbiResponse ───────────────────────────────────────────────

    private static string MessagesApiResponse(string text, string stopReason = "end_turn")
    {
        return JsonSerializer.Serialize(new
        {
            stop_reason = stopReason,
            content = new[] { new { type = "text", text } }
        });
    }

    [Fact]
    public void ParseSnbiResponse_ValidJson_ParsesAnswerAndCitations()
    {
        var text = """{"answer":"Per B.C.01, report the deck condition.","citations":[{"section":"B.C.01","quote":"Report the condition rating"}]}""";

        var result = ClaudeAnalysisService.ParseSnbiResponse(MessagesApiResponse(text));

        Assert.Null(result.Error);
        Assert.Equal("Per B.C.01, report the deck condition.", result.Answer);
        var citation = Assert.Single(result.Citations);
        Assert.Equal("B.C.01", citation.Section);
        Assert.Equal("Report the condition rating", citation.Quote);
    }

    [Fact]
    public void ParseSnbiResponse_CitationsNull_NormalizesToEmptyList()
    {
        var text = """{"answer":"Not covered in the provided sections.","citations":null}""";

        var result = ClaudeAnalysisService.ParseSnbiResponse(MessagesApiResponse(text));

        Assert.Null(result.Error);
        Assert.NotNull(result.Citations);
        Assert.Empty(result.Citations);
    }

    [Fact]
    public void ParseSnbiResponse_AnswerNull_NormalizesToEmptyString()
    {
        var text = """{"answer":null,"citations":[]}""";

        var result = ClaudeAnalysisService.ParseSnbiResponse(MessagesApiResponse(text));

        Assert.Null(result.Error);
        Assert.NotNull(result.Answer);
        Assert.Equal("", result.Answer);
    }

    [Fact]
    public void ParseSnbiResponse_FencedJson_IsUnwrappedAndParsed()
    {
        var text = "```json\n{\"answer\":\"Fenced answer\",\"citations\":[]}\n```";

        var result = ClaudeAnalysisService.ParseSnbiResponse(MessagesApiResponse(text));

        Assert.Null(result.Error);
        Assert.Equal("Fenced answer", result.Answer);
    }

    [Fact]
    public void ParseSnbiResponse_PlainTextRefusal_ReturnsErrorWithSnippet()
    {
        var result = ClaudeAnalysisService.ParseSnbiResponse(
            MessagesApiResponse("I can't help with that request."));

        Assert.NotNull(result.Error);
        Assert.Contains("unexpected response", result.Error);
        Assert.Contains("I can't help", result.Error);
    }

    [Fact]
    public void ParseSnbiResponse_MaxTokensStopReason_ReturnsTruncationError()
    {
        var result = ClaudeAnalysisService.ParseSnbiResponse(
            MessagesApiResponse("{\"answer\":\"cut off", stopReason: "max_tokens"));

        Assert.NotNull(result.Error);
        Assert.Contains("max_tokens", result.Error);
    }

    [Fact]
    public void ParseSnbiResponse_RefusalStopReason_ReturnsDeclinedError()
    {
        var result = ClaudeAnalysisService.ParseSnbiResponse(
            MessagesApiResponse("", stopReason: "refusal"));

        Assert.NotNull(result.Error);
        Assert.Contains("declined", result.Error);
    }

    [Fact]
    public void ParseSnbiResponse_EmptyContentArray_ReturnsError()
    {
        var responseJson = """{"stop_reason":"end_turn","content":[]}""";

        var result = ClaudeAnalysisService.ParseSnbiResponse(responseJson);

        Assert.NotNull(result.Error);
    }

    [Fact]
    public void ParseSnbiResponse_MalformedOuterJson_ReturnsParseError()
    {
        var result = ClaudeAnalysisService.ParseSnbiResponse("not json at all");

        Assert.NotNull(result.Error);
        Assert.Contains("Parse error", result.Error);
    }

    [Fact]
    public void ParseSnbiResponse_SkipsNonTextBlocksBeforeTextBlock()
    {
        var responseJson = JsonSerializer.Serialize(new
        {
            stop_reason = "end_turn",
            content = new object[]
            {
                new { type = "thinking", thinking = "reasoning..." },
                new { type = "text", text = """{"answer":"After thinking","citations":[]}""" }
            }
        });

        var result = ClaudeAnalysisService.ParseSnbiResponse(responseJson);

        Assert.Null(result.Error);
        Assert.Equal("After thinking", result.Answer);
    }
}
