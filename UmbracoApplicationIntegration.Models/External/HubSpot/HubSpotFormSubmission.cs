using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External.HubSpot;

public sealed class HubSpotFormSubmission
{
    [JsonPropertyName("fields")]
    public List<HubSpotFormField> Fields { get; set; } = [];

    [JsonPropertyName("pageUri")]
    public string PageUri { get; set; } = string.Empty;

    [JsonPropertyName("pageName")]
    public string PageName { get; set; } = string.Empty;
}
