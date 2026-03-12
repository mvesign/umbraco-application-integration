using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;
using UmbracoApplicationIntegration.Logic.Services;

namespace UmbracoApplicationIntegration.Logic.Composers;

public sealed class UmbazingComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<BookService>();

        builder
            .WithCollectionBuilder<MapDefinitionCollectionBuilder>()
            .Add<BookMapper>();
    }
}
