namespace UmbracoApplicationIntegration.Logic.Settings;

public sealed class AlgoliaSearchClientSettings
{
    private const string DefaultIndexName = "ITBooksIndex";
    private const int DefaultHitsPerPage = 100;

    public const string SectionKey = "Umbraco:CMS:Integrations:Search:Algolia:Settings";

    public string ApplicationId { get; set; } = string.Empty;

    public string SearchApiKey { get; set; } = string.Empty;

    public string IndexName { get; set; } = DefaultIndexName;

    public int HitsPerPage { get; set; } = DefaultHitsPerPage;
}
