using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External.HubSpot;

public sealed class HubSpotPayload
{
    [JsonPropertyName("fields")]
    public List<HubSpotFormField> Fields { get; set; } = [];

    [JsonPropertyName("context")]
    public HubSpotPayloadContext Context { get; set; } = new();
}
