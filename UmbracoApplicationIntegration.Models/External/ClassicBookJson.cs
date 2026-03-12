using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External;

public class ClassicBookJson
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("imageLink")]
    public string? ImageLink { get; set; }

    [JsonPropertyName("year")]
    public int Year { get; set; }
}
