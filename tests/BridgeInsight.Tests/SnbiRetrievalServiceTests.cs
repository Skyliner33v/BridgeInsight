using System.Net;
using System.Text;
using System.Text.Json;
using BridgeInsight.Models;
using BridgeInsight.Services;
using Xunit;

namespace BridgeInsight.Tests;

public class SnbiRetrievalServiceTests
{
    // ── Fixture corpus ──────────────────────────────────────────────────

    private static readonly object[] FixtureChunks =
    {
        new
        {
            id = "b-c-01",
            section = "B.C.01",
            title = "Deck Condition Rating",
            text = "B.C.01 Deck Condition Rating. Report the condition rating of the bridge deck. " +
                   "The deck is the portion of the bridge that directly carries traffic. " +
                   "Use the component condition rating codes in Table 20 to rate the deck."
        },
        new
        {
            id = "b-c-02",
            section = "B.C.02",
            title = "Superstructure Condition Rating",
            text = "B.C.02 Superstructure Condition Rating. Report the condition rating of the superstructure. " +
                   "The superstructure includes all primary load-carrying members supporting the deck."
        },
        new
        {
            id = "b-ie-01",
            section = "B.IE.01",
            title = "Inspection Type",
            text = "B.IE.01 Inspection Type. Report the type of inspection performed. " +
                   "Inspection types include routine inspection, in-depth inspection, damage inspection, " +
                   "and special inspection."
        },
        new
        {
            id = "section-7-1",
            section = "Section 7.1",
            title = "Component Condition Ratings",
            text = "Component condition ratings describe the severity and extent of defects. " +
                   "Codes range from 9 excellent to 0 failed. A code of 4 indicates poor condition " +
                   "with widespread moderate defects or isolated major defects."
        },
        new
        {
            id = "appendix-c-1",
            section = "Appendix C",
            title = "Condition Rating Guidance (Part 1)",
            text = "Guidance for assessing concrete defects such as cracking, spalling, and efflorescence."
        },
        new
        {
            id = "appendix-c-2",
            section = "Appendix C",
            title = "Condition Rating Guidance (Part 2)",
            text = "Guidance for assessing steel defects such as corrosion, fatigue cracking, and section loss."
        }
    };

    /// <summary>Serves the fixture corpus for data/snbi-chunks.json requests.</summary>
    private sealed class FixtureHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _json;
        public FixtureHttpMessageHandler(string json) => _json = json;

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsolutePath.EndsWith("data/snbi-chunks.json", StringComparison.Ordinal))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_json, Encoding.UTF8, "application/json")
                });
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }

    private static SnbiRetrievalService CreateService(object[]? chunks = null)
    {
        var json = JsonSerializer.Serialize(chunks ?? FixtureChunks);
        var http = new HttpClient(new FixtureHttpMessageHandler(json))
        {
            BaseAddress = new Uri("http://localhost/")
        };
        return new SnbiRetrievalService(http);
    }

    // ── Tokenization ────────────────────────────────────────────────────

    [Theory]
    [InlineData("B.C.01")]   // canonical dotted form
    [InlineData("B.C.1")]    // missing leading zero
    [InlineData("B C 01")]   // spaces as separators
    [InlineData("BC01")]     // compact form, no separators
    [InlineData("b.c.01")]   // lowercase
    public void Tokenize_NormalizesItemIdFormsToCanonicalToken(string input)
    {
        var tokens = SnbiRetrievalService.Tokenize(input);

        Assert.Contains("b_c_01", tokens);
    }

    [Fact]
    public void Tokenize_PreservesItemIdInsideSentence()
    {
        var tokens = SnbiRetrievalService.Tokenize("What does item B.C.01 say about the deck?");

        Assert.Contains("b_c_01", tokens);
        Assert.Contains("deck", tokens);
    }

    [Fact]
    public void Tokenize_JoinsDottedSectionNumbers()
    {
        var tokens = SnbiRetrievalService.Tokenize("See Section 7.1 for details");

        Assert.Contains("7_1", tokens);
        Assert.DoesNotContain("7", tokens);
        Assert.DoesNotContain("1", tokens);
    }

    [Fact]
    public void Tokenize_RemovesStopWordsAndLowercases()
    {
        var tokens = SnbiRetrievalService.Tokenize("What is the DECK of a bridge?");

        Assert.Equal(new[] { "deck", "bridge" }, tokens);
    }

    [Fact]
    public void Tokenize_KeepsSingleDigitTokensButDropsSingleLetters()
    {
        var tokens = SnbiRetrievalService.Tokenize("rating code 4 x");

        Assert.Contains("4", tokens);
        Assert.DoesNotContain("x", tokens);
    }

    [Fact]
    public void Tokenize_EmptyInput_ReturnsNoTokens()
    {
        Assert.Empty(SnbiRetrievalService.Tokenize(""));
        Assert.Empty(SnbiRetrievalService.Tokenize("   "));
    }

    // ── Retrieval ───────────────────────────────────────────────────────

    [Fact]
    public async Task RetrieveAsync_KnownQuery_RanksRelevantChunkFirst()
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync("How is the deck condition rating reported?");

        Assert.NotEmpty(hits);
        Assert.Equal("B.C.01", hits[0].Chunk.Section);
    }

    [Theory]
    [InlineData("B.C.01")]
    [InlineData("BC01")]
    [InlineData("B.C.1")]
    public async Task RetrieveAsync_ItemIdQueryForms_AllFindTheItem(string query)
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync(query);

        Assert.NotEmpty(hits);
        Assert.Equal("B.C.01", hits[0].Chunk.Section);
    }

    [Fact]
    public async Task RetrieveAsync_InspectionQuery_FindsInspectionChunk()
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync("What inspection types are defined?");

        Assert.NotEmpty(hits);
        Assert.Equal("B.IE.01", hits[0].Chunk.Section);
    }

    [Fact]
    public async Task RetrieveAsync_ScoresAreOrderedDescending()
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync("condition rating of the deck and superstructure");

        Assert.True(hits.Count >= 2);
        for (var i = 1; i < hits.Count; i++)
            Assert.True(hits[i - 1].Score >= hits[i].Score);
    }

    [Fact]
    public async Task RetrieveAsync_RespectsTopK()
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync("condition rating", topK: 2);

        Assert.True(hits.Count <= 2);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("the of and is a")] // stop words only
    public async Task RetrieveAsync_EmptyOrStopWordQuery_ReturnsEmpty(string query)
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync(query);

        Assert.Empty(hits);
    }

    [Fact]
    public async Task RetrieveAsync_NoMatchingTerms_ReturnsEmpty()
    {
        var service = CreateService();

        var hits = await service.RetrieveAsync("zebra quantum marmalade");

        Assert.Empty(hits);
    }

    [Fact]
    public async Task RetrieveAsync_EmptyCorpus_ReturnsEmpty()
    {
        var service = CreateService(Array.Empty<object>());

        var hits = await service.RetrieveAsync("deck condition");

        Assert.Empty(hits);
    }

    [Fact]
    public async Task EnsureLoadedAsync_PopulatesChunkCount()
    {
        var service = CreateService();
        Assert.False(service.IsLoaded);

        await service.EnsureLoadedAsync();

        Assert.True(service.IsLoaded);
        Assert.Equal(FixtureChunks.Length, service.ChunkCount);
    }

    // ── FindBySection ───────────────────────────────────────────────────

    [Fact]
    public async Task FindBySection_MatchesCaseInsensitively()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        var chunk = service.FindBySection("b.c.01");

        Assert.NotNull(chunk);
        Assert.Equal("Deck Condition Rating", chunk!.Title);
    }

    [Fact]
    public async Task FindBySection_MultiChunkSection_PrefersChunkContainingQuote()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        var chunk = service.FindBySection("Appendix C", "steel   defects such as CORROSION");

        Assert.NotNull(chunk);
        Assert.Equal("appendix-c-2", chunk!.Id);
    }

    [Fact]
    public async Task FindBySection_UnknownSection_ReturnsNull()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        Assert.Null(service.FindBySection("B.ZZ.99"));
    }

    // ── VerifyQuote ─────────────────────────────────────────────────────

    [Fact]
    public async Task VerifyQuote_ExactSubstring_ReturnsTrue()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        Assert.True(service.VerifyQuote("B.C.01", "Report the condition rating of the bridge deck."));
    }

    [Fact]
    public async Task VerifyQuote_WhitespaceAndCaseDifferences_ReturnsTrue()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        Assert.True(service.VerifyQuote("B.C.01", "report the  condition\nrating of THE bridge deck"));
    }

    [Fact]
    public async Task VerifyQuote_FabricatedQuote_ReturnsFalse()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        Assert.False(service.VerifyQuote("B.C.01", "The deck shall be replaced every ten years."));
    }

    [Fact]
    public async Task VerifyQuote_QuoteFromDifferentSection_ReturnsFalse()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        // The quote is real but belongs to B.C.02, not the cited B.C.01
        Assert.False(service.VerifyQuote("B.C.01", "The superstructure includes all primary load-carrying members"));
    }

    [Fact]
    public async Task VerifyQuote_UnknownSection_ReturnsFalse()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        Assert.False(service.VerifyQuote("B.ZZ.99", "Report the condition rating"));
    }

    [Fact]
    public async Task VerifyQuote_EmptyQuote_ReturnsFalse()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        Assert.False(service.VerifyQuote("B.C.01", ""));
        Assert.False(service.VerifyQuote("B.C.01", "   "));
    }

    [Fact]
    public async Task VerifyQuote_MultiChunkSection_MatchesQuoteInLaterChunk()
    {
        var service = CreateService();
        await service.EnsureLoadedAsync();

        // The quote lives in the second Appendix C chunk
        Assert.True(service.VerifyQuote("Appendix C", "corrosion, fatigue cracking, and section loss"));
    }

    [Fact]
    public void VerifyQuote_CorpusNotLoaded_ReturnsNull()
    {
        var service = CreateService();

        Assert.Null(service.VerifyQuote("B.C.01", "Report the condition rating"));
    }
}
