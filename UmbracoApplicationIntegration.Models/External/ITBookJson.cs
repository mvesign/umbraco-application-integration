using System.Text.Json.Serialization;

namespace UmbracoApplicationIntegration.Models.External;

public class ITBookJson
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; } = [];

    [JsonPropertyName("thumbnailUrl")]
    public string? ThumbnailUrl { get; set; }
    
    [JsonPropertyName("publishedDate")]
    public PublishedDate? PublishedDate { get; set; }
}
