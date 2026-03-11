namespace BridgeInsight.Models;

public class Bridge
{
    public int Id { get; set; }
    public string StateCode { get; set; } = "";
    public string StructureNumber { get; set; } = "";
    public string FeaturesIntersected { get; set; } = "";
    public string FacilityCarried { get; set; } = "";
    public string CountyCode { get; set; } = "";
    public string CountyName { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    // Structure Type
    public string MainSpanMaterial { get; set; } = "";
    public string MainSpanDesign { get; set; } = "";
    public string ServiceOnBridge { get; set; } = "";
    public string ServiceUnderBridge { get; set; } = "";

    // Age
    public int? YearBuilt { get; set; }
    public int? YearReconstructed { get; set; }

    // Condition Ratings (0-9)
    public int? DeckCondition { get; set; }
    public int? SuperstructureCondition { get; set; }
    public int? SubstructureCondition { get; set; }
    public int? CulvertCondition { get; set; }
    public int? ChannelCondition { get; set; }
    public int? WaterwayAdequacy { get; set; }

    // Traffic
    public int? AverageDailyTraffic { get; set; }
    public int? TruckTrafficPercent { get; set; }

    // Geometry
    public double? StructureLength { get; set; }
    public double? BridgeRoadwayWidth { get; set; }
    public double? ApproachRoadwayWidth { get; set; }

    // Appraisal
    public string StructuralEvaluation { get; set; } = "";
    public string DeckGeometryEvaluation { get; set; } = "";
    public string UnderclearanceEvaluation { get; set; } = "";
    public string ApproachRoadwayAlignment { get; set; } = "";

    // Status
    public string OpenPostedClosed { get; set; } = "";
    public string NbisBridgeLength { get; set; } = "";
    public string ScourCritical { get; set; } = "";
    public int? BridgePosting { get; set; }

    // Ownership & Maintenance
    public string Owner { get; set; } = "";
    public string MaintenanceResponsibility { get; set; } = "";

    // Inspection
    public DateTime? InspectionDate { get; set; }
    public int? InspectionFrequency { get; set; }

    // Computed properties
    public int Age => DateTime.Now.Year - (YearBuilt ?? DateTime.Now.Year);

    public int? LowestConditionRating
    {
        get
        {
            var ratings = new[] { DeckCondition, SuperstructureCondition, SubstructureCondition, CulvertCondition }
                .Where(r => r.HasValue)
                .Select(r => r!.Value);
            return ratings.Any() ? ratings.Min() : null;
        }
    }

    public string OverallCondition
    {
        get
        {
            var lowest = LowestConditionRating;
            if (lowest == null) return "N/A";
            return lowest switch
            {
                >= 7 => "Good",
                >= 5 => "Fair",
                >= 4 => "Poor",
                _ => "Serious/Critical"
            };
        }
    }

    public bool IsStructurallyDeficient =>
        DeckCondition is <= 4 ||
        SuperstructureCondition is <= 4 ||
        SubstructureCondition is <= 4 ||
        CulvertCondition is <= 4;

    public bool IsInspectionOverdue
    {
        get
        {
            if (InspectionDate == null || InspectionFrequency == null) return false;
            var nextDue = InspectionDate.Value.AddMonths(InspectionFrequency.Value);
            return DateTime.Now > nextDue;
        }
    }
}
