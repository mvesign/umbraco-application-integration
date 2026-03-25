namespace UmbracoApplicationIntegration.Logic.Settings;

public sealed class HubSpotClientSettings
{
    public const string SectionKey = "Umbraco:Integrations:Crm:Hubspot:Settings";

    public string ApiKey { get; set; } = string.Empty;

    public string PortalId { get; set; } = string.Empty;

    public string FormId { get; set; } = string.Empty;

    public string Region { get; set; } = string.Empty;
}
