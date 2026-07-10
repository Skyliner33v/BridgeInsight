namespace BridgeInsight.Models;

/// <summary>
/// A section-level excerpt of the FHWA Specifications for the National Bridge
/// Inventory (SNBI), extracted at build time by tools/preprocess_snbi.py.
/// </summary>
public class SnbiChunk
{
    public string Id { get; set; } = "";
    public string Section { get; set; } = "";
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
}

/// <summary>A chunk ranked against a user query with its lexical score.</summary>
public class SnbiRetrievalHit
{
    public SnbiChunk Chunk { get; set; } = new();
    public double Score { get; set; }
}

/// <summary>Structured, citation-bearing answer to an SNBI question.</summary>
public class SnbiAnswerResult
{
    public string Answer { get; set; } = "";
    public List<SnbiCitation> Citations { get; set; } = new();
    public bool IsDemo { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public string? Error { get; set; }
}

public class SnbiCitation
{
    public string Section { get; set; } = "";
    public string Quote { get; set; } = "";
}
