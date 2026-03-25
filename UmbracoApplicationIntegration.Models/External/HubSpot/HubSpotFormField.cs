using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External.HubSpot;

public sealed class HubSpotFormField
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
