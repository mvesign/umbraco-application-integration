namespace UmbracoApplicationIntegration.Models;

public sealed class Review
{
    public string BookId { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public string Reviewer { get; set; } = string.Empty;
}
