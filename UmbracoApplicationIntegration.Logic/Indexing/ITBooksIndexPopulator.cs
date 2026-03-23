using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Examine;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoApplicationIntegration.Logic.Settings;
using UmbracoApplicationIntegration.Models.Indexing;

namespace UmbracoApplicationIntegration.Logic.Indexing;

public sealed class ITBooksIndexPopulator : IndexPopulator
{
    private readonly AlgoliaSearchClientSettings _algoliaSearchClientSettings;
    private readonly ITBooksIndexValueSetBuilder _itBookValueSetBuilder;

    public ITBooksIndexPopulator(
        IOptions<AlgoliaSearchClientSettings> algoliaSearchClientSettings,
        ITBooksIndexValueSetBuilder itBookValueSetBuilder)
    {
        _algoliaSearchClientSettings = algoliaSearchClientSettings.Value;
        _itBookValueSetBuilder = itBookValueSetBuilder;

        RegisterIndex(ITBooksIndex.IndexName);
    }

    protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
    {
        var client = new SearchClient(
            _algoliaSearchClientSettings.ApplicationId,
            _algoliaSearchClientSettings.SearchApiKey);
        
        var index = client.InitIndex(_algoliaSearchClientSettings.IndexName);

        var searchResult = index.Search<ITBookModel>(new Query
        {
            SearchQuery = string.Empty,
            HitsPerPage = _algoliaSearchClientSettings.HitsPerPage
        });
        if (searchResult?.Hits is null
            || searchResult.Hits.Count == 0)
        {
            return;
        }

        var data = searchResult.Hits.ToArray();

        foreach (var item in indexes)
        {
            item.IndexItems(_itBookValueSetBuilder.GetValueSets(data));
        }
    }
}
