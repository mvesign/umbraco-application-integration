using Examine;
using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoApplicationIntegration.Logic.Indexing;
using UmbracoApplicationIntegration.Logic.Mappers;
using UmbracoApplicationIntegration.Logic.Services;
using UmbracoApplicationIntegration.Logic.Settings;

namespace UmbracoApplicationIntegration.Logic.Composers;

public sealed class UmbazingComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<AlgoliaSearchClientSettings>(
            builder.Config.GetSection(AlgoliaSearchClientSettings.SectionKey));
        builder.Services.Configure<HubSpotClientSettings>(
            builder.Config.GetSection(HubSpotClientSettings.SectionKey));

        builder.Services.AddScoped<BookService>();

        builder
            .WithCollectionBuilder<MapDefinitionCollectionBuilder>()
            .Add<BookMapper>()
            .Add<HubSpotMapper>();

        // Configure IT books index logic.
        builder.Services.AddSingleton<ITBooksIndexValueSetBuilder>();
        builder.Services.AddSingleton<IIndexPopulator, ITBooksIndexPopulator>();
        builder.Services.AddExamineLuceneIndex<ITBooksIndex, ConfigurationEnabledDirectoryFactory>(
            ITBooksIndex.IndexName);
    }
}
