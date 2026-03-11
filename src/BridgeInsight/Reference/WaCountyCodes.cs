namespace BridgeInsight.Reference;

public static class WaCountyCodes
{
    public static readonly IReadOnlyDictionary<string, string> Codes = new Dictionary<string, string>
    {
        ["001"] = "Adams",
        ["003"] = "Asotin",
        ["005"] = "Benton",
        ["007"] = "Chelan",
        ["009"] = "Clallam",
        ["011"] = "Clark",
        ["013"] = "Columbia",
        ["015"] = "Cowlitz",
        ["017"] = "Douglas",
        ["019"] = "Ferry",
        ["021"] = "Franklin",
        ["023"] = "Garfield",
        ["025"] = "Grant",
        ["027"] = "Grays Harbor",
        ["029"] = "Island",
        ["031"] = "Jefferson",
        ["033"] = "King",
        ["035"] = "Kitsap",
        ["037"] = "Kittitas",
        ["039"] = "Klickitat",
        ["041"] = "Lewis",
        ["043"] = "Lincoln",
        ["045"] = "Mason",
        ["047"] = "Okanogan",
        ["049"] = "Pacific",
        ["051"] = "Pend Oreille",
        ["053"] = "Pierce",
        ["055"] = "San Juan",
        ["057"] = "Skagit",
        ["059"] = "Skamania",
        ["061"] = "Snohomish",
        ["063"] = "Spokane",
        ["065"] = "Stevens",
        ["067"] = "Thurston",
        ["069"] = "Wahkiakum",
        ["071"] = "Walla Walla",
        ["073"] = "Whatcom",
        ["075"] = "Whitman",
        ["077"] = "Yakima"
    };

    public static string GetCountyName(string code)
        => Codes.TryGetValue(code, out var name) ? name : $"Unknown ({code})";
}
