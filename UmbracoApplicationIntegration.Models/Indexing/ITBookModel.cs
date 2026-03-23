namespace UmbracoApplicationIntegration.Models.Indexing;

public sealed class ITBookModel
{
    public string ObjectID { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public int PageCount { get; set; }

    public IDictionary<string, object> PublishedDate { get; set; } = new Dictionary<string, object>();

    public string ThumbnailUrl { get; set; } = string.Empty;
    
    public string ShortDescription { get; set; } = string.Empty;
    
    public string LongDescription { get; set; } = string.Empty;

    public List<string> Authors { get; set; } = [];

    public List<string> Categories { get; set; } = [];
}
