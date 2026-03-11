namespace BridgeInsight.Reference;

public static class OwnerCodes
{
    public static readonly IReadOnlyDictionary<string, string> Owners = new Dictionary<string, string>
    {
        ["01"] = "State Highway Agency",
        ["02"] = "County Highway Agency",
        ["03"] = "Town or Township Highway Agency",
        ["04"] = "City or Municipal Highway Agency",
        ["11"] = "State Park, Forest, or Reservation Agency",
        ["12"] = "Local Park, Forest, or Reservation Agency",
        ["21"] = "Other State Agencies",
        ["25"] = "Other Local Agencies",
        ["26"] = "Private (other than railroad)",
        ["27"] = "Railroad",
        ["31"] = "State Toll Authority",
        ["32"] = "Local Toll Authority",
        ["60"] = "Other Federal Agencies",
        ["62"] = "Bureau of Indian Affairs",
        ["64"] = "U.S. Forest Service",
        ["66"] = "National Park Service",
        ["68"] = "Bureau of Land Management",
        ["69"] = "Bureau of Reclamation",
        ["70"] = "Military Reservation / Corps of Engineers",
        ["80"] = "Unknown"
    };

    public static readonly IReadOnlyDictionary<string, string> MaintenanceResponsibilities = new Dictionary<string, string>
    {
        ["01"] = "State Highway Agency",
        ["02"] = "County Highway Agency",
        ["03"] = "Town or Township Highway Agency",
        ["04"] = "City or Municipal Highway Agency",
        ["11"] = "State Park, Forest, or Reservation Agency",
        ["12"] = "Local Park, Forest, or Reservation Agency",
        ["21"] = "Other State Agencies",
        ["25"] = "Other Local Agencies",
        ["26"] = "Private (other than railroad)",
        ["27"] = "Railroad",
        ["31"] = "State Toll Authority",
        ["32"] = "Local Toll Authority",
        ["60"] = "Other Federal Agencies",
        ["62"] = "Bureau of Indian Affairs",
        ["64"] = "U.S. Forest Service",
        ["66"] = "National Park Service",
        ["68"] = "Bureau of Land Management",
        ["69"] = "Bureau of Reclamation",
        ["70"] = "Military Reservation / Corps of Engineers",
        ["80"] = "Unknown"
    };

    public static string GetOwner(string code) =>
        Owners.TryGetValue(code, out var name) ? name : $"Unknown ({code})";

    public static string GetMaintenanceResponsibility(string code) =>
        MaintenanceResponsibilities.TryGetValue(code, out var name) ? name : $"Unknown ({code})";
}
