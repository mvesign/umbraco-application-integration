namespace UmbracoApplicationIntegration.Models;

public sealed class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public int Year { get; set; }

    public List<Review> Reviews { get; set; } = [];
}
