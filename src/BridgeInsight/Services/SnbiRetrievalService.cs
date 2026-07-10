using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using BridgeInsight.Models;

namespace BridgeInsight.Services;

/// <summary>
/// Fully client-side retrieval over the SNBI specification chunks.
/// Loads wwwroot/data/snbi-chunks.json once and ranks chunks against a query
/// using simple lexical scoring (normalized term frequency with an inverse
/// document frequency weight) — no embeddings, no server.
/// </summary>
public class SnbiRetrievalService
{
    private readonly HttpClient _http;

    private List<SnbiChunk>? _chunks;
    private List<Dictionary<string, int>>? _chunkTermFrequencies;
    private List<int>? _chunkLengths;
    private Dictionary<string, int>? _documentFrequencies;
    private double _averageChunkLength;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private static readonly HashSet<string> StopWords = new(StringComparer.Ordinal)
    {
        "a", "an", "and", "are", "as", "at", "be", "been", "but", "by", "can",
        "do", "does", "for", "from", "has", "have", "how", "if", "in", "into",
        "is", "it", "its", "may", "must", "no", "not", "of", "on", "or", "shall",
        "should", "than", "that", "the", "their", "then", "there", "these",
        "this", "to", "use", "used", "using", "was", "were", "what", "when",
        "where", "which", "who", "why", "will", "with", "would"
    };

    // Matches SNBI item identifiers (e.g. "B.C.01") so they survive tokenization
    private static readonly Regex ItemIdRegex = new(@"\bb\.([a-z]{1,3})\.(\d{2})\b", RegexOptions.Compiled);
    private static readonly Regex NonTokenRegex = new(@"[^a-z0-9_]+", RegexOptions.Compiled);

    public SnbiRetrievalService(HttpClient http)
    {
        _http = http;
    }

    public bool IsLoaded => _chunks != null;
    public int ChunkCount => _chunks?.Count ?? 0;

    public async Task EnsureLoadedAsync()
    {
        if (_chunks != null) return;

        var chunks = await _http.GetFromJsonAsync<List<SnbiChunk>>("data/snbi-chunks.json", JsonOptions)
                     ?? new List<SnbiChunk>();
        BuildIndex(chunks);
    }

    /// <summary>Returns the top-K chunks for a query, ranked by lexical score.</summary>
    public async Task<List<SnbiRetrievalHit>> RetrieveAsync(string query, int topK = 5)
    {
        await EnsureLoadedAsync();
        if (_chunks == null || _chunks.Count == 0 || string.IsNullOrWhiteSpace(query))
            return new List<SnbiRetrievalHit>();

        var queryTerms = Tokenize(query)
            .GroupBy(t => t)
            .ToDictionary(g => g.Key, g => g.Count());
        if (queryTerms.Count == 0)
            return new List<SnbiRetrievalHit>();

        var hits = new List<SnbiRetrievalHit>();
        for (var i = 0; i < _chunks.Count; i++)
        {
            var score = ScoreChunk(i, queryTerms);
            if (score > 0)
                hits.Add(new SnbiRetrievalHit { Chunk = _chunks[i], Score = score });
        }

        return hits
            .OrderByDescending(h => h.Score)
            .Take(topK)
            .ToList();
    }

    /// <summary>
    /// Looks up a chunk by its section identifier (e.g. "B.C.01"). When a
    /// section spans multiple chunks and a quote is supplied, prefers the
    /// chunk that actually contains the quote (whitespace-insensitive).
    /// </summary>
    public SnbiChunk? FindBySection(string section, string? quote = null)
    {
        if (_chunks == null || string.IsNullOrWhiteSpace(section)) return null;
        var trimmed = section.Trim();

        var matches = _chunks
            .Where(c => string.Equals(c.Section, trimmed, StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (matches.Count == 0)
        {
            matches = _chunks
                .Where(c => string.Equals(c.Id, trimmed, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        if (matches.Count == 0) return null;

        if (matches.Count > 1 && !string.IsNullOrWhiteSpace(quote))
        {
            var normalizedQuote = NormalizeWhitespace(quote);
            var containing = matches.FirstOrDefault(c => NormalizeWhitespace(c.Text).Contains(normalizedQuote));
            if (containing != null) return containing;
        }

        return matches[0];
    }

    private static string NormalizeWhitespace(string text)
        => Regex.Replace(text, @"\s+", " ").Trim().ToLowerInvariant();

    private void BuildIndex(List<SnbiChunk> chunks)
    {
        var termFrequencies = new List<Dictionary<string, int>>(chunks.Count);
        var lengths = new List<int>(chunks.Count);
        var documentFrequencies = new Dictionary<string, int>(StringComparer.Ordinal);
        long totalLength = 0;

        foreach (var chunk in chunks)
        {
            var tf = new Dictionary<string, int>(StringComparer.Ordinal);

            foreach (var token in Tokenize(chunk.Text))
                tf[token] = tf.GetValueOrDefault(token) + 1;

            // Section id and title terms are strong signals — weight them up
            foreach (var token in Tokenize(chunk.Section + " " + chunk.Title))
                tf[token] = tf.GetValueOrDefault(token) + 3;

            foreach (var term in tf.Keys)
                documentFrequencies[term] = documentFrequencies.GetValueOrDefault(term) + 1;

            var length = tf.Values.Sum();
            termFrequencies.Add(tf);
            lengths.Add(length);
            totalLength += length;
        }

        _chunks = chunks;
        _chunkTermFrequencies = termFrequencies;
        _chunkLengths = lengths;
        _documentFrequencies = documentFrequencies;
        _averageChunkLength = chunks.Count > 0 ? (double)totalLength / chunks.Count : 1;
    }

    private double ScoreChunk(int chunkIndex, Dictionary<string, int> queryTerms)
    {
        const double k1 = 1.4;   // term-frequency saturation
        const double b = 0.6;    // length normalization strength

        var tf = _chunkTermFrequencies![chunkIndex];
        var lengthNorm = _chunkLengths![chunkIndex] / _averageChunkLength;
        var totalChunks = _chunks!.Count;

        double score = 0;
        foreach (var (term, queryCount) in queryTerms)
        {
            if (!tf.TryGetValue(term, out var frequency)) continue;

            var docFrequency = _documentFrequencies!.GetValueOrDefault(term, 1);
            var idf = Math.Log(1 + (totalChunks - docFrequency + 0.5) / (docFrequency + 0.5));
            var tfNorm = frequency * (k1 + 1) / (frequency + k1 * (1 - b + b * lengthNorm));
            score += queryCount * idf * tfNorm;
        }

        return score;
    }

    private static List<string> Tokenize(string text)
    {
        var lower = text.ToLowerInvariant();
        // Preserve item identifiers like "B.C.01" as single tokens ("b_c_01")
        lower = ItemIdRegex.Replace(lower, "b_$1_$2");

        return NonTokenRegex.Split(lower)
            .Where(t => t.Length >= 2 && !StopWords.Contains(t))
            .ToList();
    }
}
