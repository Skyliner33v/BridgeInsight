namespace BridgeInsight.Models;

public class BridgeAnalysis
{
    public string Summary { get; set; } = "";
    public List<EvidenceItem> EvidenceChain { get; set; } = new();
    public List<RiskFactor> RiskFactors { get; set; } = new();
    public List<string> DataGaps { get; set; } = new();
    public List<RecommendedAction> RecommendedActions { get; set; } = new();
    public bool IsDemo { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public string? Error { get; set; }
}

public class EvidenceItem
{
    public string Claim { get; set; } = "";
    public List<string> DataFields { get; set; } = new();
    public string StandardReference { get; set; } = "";
    public string Reasoning { get; set; } = "";
}

public class RiskFactor
{
    public string Factor { get; set; } = "";
    public string Severity { get; set; } = ""; // high, medium, low
    public string Detail { get; set; } = "";
}

public class RecommendedAction
{
    public int Priority { get; set; }
    public string Action { get; set; } = "";
    public string Rationale { get; set; } = "";
}
