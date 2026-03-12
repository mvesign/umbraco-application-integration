using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External;

public class PublishedDate
{
    [JsonPropertyName("$date")]
    public string? Date { get; set; }
}
