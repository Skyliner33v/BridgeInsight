namespace BridgeInsight.Models;

public class PortfolioBriefingResult
{
    public string ExecutiveSummary { get; set; } = "";
    public RiskTiers RiskTiers { get; set; } = new();
    public ComparativeAnalysis ComparativeAnalysis { get; set; } = new();
    public List<BridgeEvidenceBlock> BridgeEvidenceBlocks { get; set; } = new();
    public string FundingNarrative { get; set; } = "";
    public List<string> DataQualityNotes { get; set; } = new();
    public bool IsDemo { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public string? Error { get; set; }
}

public class RiskTiers
{
    public RiskTier ImmediateAttention { get; set; } = new();
    public RiskTier NearTermPriority { get; set; } = new();
    public RiskTier Monitor { get; set; } = new();
    public RiskTier Satisfactory { get; set; } = new();
}

public class RiskTier
{
    public List<string> Bridges { get; set; } = new();
    public string Rationale { get; set; } = "";
}

public class ComparativeAnalysis
{
    public List<string> Patterns { get; set; } = new();
    public List<string> PrevalentRiskFactors { get; set; } = new();
    public List<string> DataGaps { get; set; } = new();
}

public class BridgeEvidenceBlock
{
    public string StructureNumber { get; set; } = "";
    public string Facility { get; set; } = "";
    public Dictionary<string, int?> KeyRatings { get; set; } = new();
    public string OneSentenceAssessment { get; set; } = "";
    public List<string> RiskFactors { get; set; } = new();
    public string ActionCategory { get; set; } = "";
}

public class BridgeSearchCriteria
{
    public string? SearchText { get; set; }
    public string? CountyCode { get; set; }
    public int? MinCondition { get; set; }
    public int? MaxCondition { get; set; }
    public int? MinYearBuilt { get; set; }
    public int? MaxYearBuilt { get; set; }
    public int? MinAdt { get; set; }
    public int? MaxAdt { get; set; }
    public string? StructureType { get; set; }
    public bool StructurallyDeficientOnly { get; set; }
    public bool ScourCriticalOnly { get; set; }
    public string SortBy { get; set; } = "FacilityCarried";
    public bool SortDescending { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}

public class BridgeSearchResult
{
    public List<Bridge> Bridges { get; set; } = new();
    public int TotalCount { get; set; }
    public double AverageAge { get; set; }
    public double PercentStructurallyDeficient { get; set; }
    public double AverageAdt { get; set; }
}
