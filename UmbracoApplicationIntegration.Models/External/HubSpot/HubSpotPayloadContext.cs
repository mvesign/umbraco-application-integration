using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External.HubSpot;

public sealed class HubSpotPayloadContext
{
    [JsonPropertyName("pageUri")]
    public string PageUri { get; set; } = string.Empty;

    [JsonPropertyName("pageName")]
    public string PageName { get; set; } = string.Empty;
}
