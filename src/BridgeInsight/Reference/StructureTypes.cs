namespace BridgeInsight.Reference;

public static class StructureTypes
{
    public static readonly IReadOnlyDictionary<string, string> Materials = new Dictionary<string, string>
    {
        ["1"] = "Concrete",
        ["2"] = "Concrete Continuous",
        ["3"] = "Steel",
        ["4"] = "Steel Continuous",
        ["5"] = "Prestressed Concrete",
        ["6"] = "Prestressed Concrete Continuous",
        ["7"] = "Wood or Timber",
        ["8"] = "Masonry",
        ["9"] = "Aluminum, Wrought Iron, or Cast Iron",
        ["0"] = "Other"
    };

    public static readonly IReadOnlyDictionary<string, string> Designs = new Dictionary<string, string>
    {
        ["01"] = "Slab",
        ["02"] = "Stringer/Multi-beam or Girder",
        ["03"] = "Girder and Floorbeam System",
        ["04"] = "Tee Beam",
        ["05"] = "Box Beam or Girders - Multiple",
        ["06"] = "Box Beam or Girders - Single or Spread",
        ["07"] = "Frame (except frame culverts)",
        ["08"] = "Orthotropic",
        ["09"] = "Truss - Deck",
        ["10"] = "Truss - Thru",
        ["11"] = "Arch - Deck",
        ["12"] = "Arch - Thru",
        ["13"] = "Suspension",
        ["14"] = "Stayed Girder",
        ["15"] = "Movable - Lift",
        ["16"] = "Movable - Bascule",
        ["17"] = "Movable - Swing",
        ["18"] = "Tunnel",
        ["19"] = "Culvert (includes frame culverts)",
        ["20"] = "Mixed Types",
        ["21"] = "Segmental Box Girder",
        ["22"] = "Channel Beam",
        ["00"] = "Other"
    };

    public static readonly IReadOnlyDictionary<string, string> ServiceOn = new Dictionary<string, string>
    {
        ["1"] = "Highway",
        ["2"] = "Railroad",
        ["3"] = "Pedestrian-Bicycle",
        ["4"] = "Highway-Railroad",
        ["5"] = "Highway-Pedestrian",
        ["6"] = "Overpass Structure at an Interchange",
        ["7"] = "Third Level (Interchange)",
        ["8"] = "Fourth Level (Interchange)",
        ["9"] = "Building or Plaza",
        ["0"] = "Other"
    };

    public static readonly IReadOnlyDictionary<string, string> ServiceUnder = new Dictionary<string, string>
    {
        ["1"] = "Highway, with or without pedestrian",
        ["2"] = "Railroad",
        ["3"] = "Pedestrian-Bicycle",
        ["4"] = "Highway-Railroad",
        ["5"] = "Waterway",
        ["6"] = "Highway-Waterway",
        ["7"] = "Railroad-Waterway",
        ["8"] = "Highway-Waterway-Railroad",
        ["9"] = "Relief for Waterway",
        ["0"] = "Other"
    };

    public static string GetMaterial(string code) =>
        Materials.TryGetValue(code, out var name) ? name : $"Unknown ({code})";

    public static string GetDesign(string code) =>
        Designs.TryGetValue(code, out var name) ? name : $"Unknown ({code})";

    public static string GetServiceOn(string code) =>
        ServiceOn.TryGetValue(code, out var name) ? name : $"Unknown ({code})";

    public static string GetServiceUnder(string code) =>
        ServiceUnder.TryGetValue(code, out var name) ? name : $"Unknown ({code})";

    public static string GetFullDescription(string materialCode, string designCode) =>
        $"{GetMaterial(materialCode)} {GetDesign(designCode)}";
}
