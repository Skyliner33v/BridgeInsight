namespace BridgeInsight.Reference;

public static class FhwaRatings
{
    public static readonly IReadOnlyDictionary<int, string> ConditionRatings = new Dictionary<int, string>
    {
        [9] = "Excellent Condition",
        [8] = "Very Good Condition — no problems noted",
        [7] = "Good Condition — some minor problems",
        [6] = "Satisfactory Condition — structural elements show some minor deterioration",
        [5] = "Fair Condition — all primary structural elements are sound but may have minor section loss, cracking, spalling, or scour",
        [4] = "Poor Condition — advanced section loss, deterioration, spalling, or scour",
        [3] = "Serious Condition — loss of section, deterioration, spalling, or scour have seriously affected primary structural components",
        [2] = "Critical Condition — advanced deterioration of primary structural elements. Shoring may be necessary",
        [1] = "Imminent Failure Condition — major deterioration or section loss in critical structural components. Bridge is closed but corrective action may put it back in light service",
        [0] = "Failed Condition — out of service, beyond corrective action"
    };

    public static readonly IReadOnlyDictionary<int, string> ConditionLabels = new Dictionary<int, string>
    {
        [9] = "Excellent",
        [8] = "Very Good",
        [7] = "Good",
        [6] = "Satisfactory",
        [5] = "Fair",
        [4] = "Poor",
        [3] = "Serious",
        [2] = "Critical",
        [1] = "Imminent Failure",
        [0] = "Failed"
    };

    public static string GetRatingDescription(int? rating)
        => rating.HasValue && ConditionRatings.TryGetValue(rating.Value, out var desc) ? desc : "Not Rated";

    public static string GetRatingLabel(int? rating)
        => rating.HasValue && ConditionLabels.TryGetValue(rating.Value, out var label) ? label : "N/A";

    public static string GetRatingCssClass(int? rating)
    {
        if (rating == null) return "rating-na";
        return rating switch
        {
            >= 7 => "rating-good",
            >= 5 => "rating-fair",
            4 => "rating-poor",
            _ => "rating-critical"
        };
    }

    public static string GetFullReferenceText()
    {
        return @"FHWA NBI Condition Rating Scale (Items 58, 59, 60, 62):
9 = Excellent Condition
8 = Very Good Condition — no problems noted
7 = Good Condition — some minor problems
6 = Satisfactory Condition — structural elements show some minor deterioration
5 = Fair Condition — all primary structural elements are sound but may have minor section loss, cracking, spalling, or scour
4 = Poor Condition — advanced section loss, deterioration, spalling, or scour
3 = Serious Condition — loss of section, deterioration, spalling, or scour have seriously affected primary structural components
2 = Critical Condition — advanced deterioration of primary structural elements. Shoring may be necessary
1 = Imminent Failure Condition — major deterioration or section loss in critical structural components. Bridge is closed but corrective action may put it back in light service
0 = Failed Condition — out of service, beyond corrective action

A bridge is classified as Structurally Deficient when any of the following condition ratings (Deck, Superstructure, Substructure, or Culvert) is 4 or below.

Channel/Waterway Condition (Item 61):
9 = No noticeable or noteworthy deficiencies
8 = Banks are protected or well vegetated
7 = Bank protection is in need of minor repairs
6 = Bank is beginning to slump
5 = Bank protection is being undermined
4 = Bank and embankment protection is severely undermined
3 = Bank protection has failed
2 = The channel has changed enough to affect the bridge
1 = Bridge is closed because of channel failure
0 = Bridge is closed because of channel failure and is beyond repair

Scour Critical Bridges (Item 113):
N = Bridge not over waterway
U = Unknown foundation
T = Bridge over tidal waters — unknown foundation
9 = Bridge foundations well above flood water elevations
8 = Bridge foundations determined to be stable (scour countermeasures in place)
7 = Countermeasures have been installed and are functioning properly
6 = Scour calculation/evaluation has not been made
5 = Bridge foundations determined to be stable
4 = Bridge foundations determined to be stable — action required to protect
3 = Bridge is scour critical — unstable; corrective action required
2 = Bridge is scour critical — immediate action required
1 = Bridge is scour critical — failure is imminent
0 = Bridge has failed and is closed";
    }
}
