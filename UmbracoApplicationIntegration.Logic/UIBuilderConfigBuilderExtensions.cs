using Umbraco.UIBuilder.Configuration.Builders;
using Umbraco.UIBuilder.Infrastructure.Configuration.Actions;
using UmbracoApplicationIntegration.Logic.Services;
using UmbracoApplicationIntegration.Models;

namespace UmbracoApplicationIntegration.Logic;

public static class UIBuilderConfigBuilderExtensions
{
    public static void AddRepositoriesSection(this UIBuilderConfigBuilder builder) =>
        builder.AddSection("Repositories", sectionConfig =>
        {
            sectionConfig.Tree(treeConfig =>
            {
                treeConfig.AddCollection<Book>(x =>
                    x.Id,
                    "Book",
                    "Books",
                    "A collection of books",
                    "icon-book",
                    "icon-books",
                    collectionConfig =>
                    {
                        collectionConfig
                            .SetAlias("books")
                            .SetNameProperty(x => x.Title)
                            .ListView(listViewConfig =>
                                listViewConfig
                                    .AddField(x => x.Author).SetHeading("Author")
                                    .AddField(x => x.Year).SetHeading("Year Published")
                                    .AddField(x => x.Image).SetHeading("Image"))
                            .AddSearchableProperty(x => x.Title)
                            .AddAction<ImportEntityAction>()
                            .AddAction<ExportEntityAction>()
                            .SetRepositoryType<BookRepository>()
                            .Editor(editorConfig =>
                                editorConfig.AddTab(
                                    "General",
                                    tabConfig => tabConfig.AddFieldset(
                                        "General",
                                        fieldsetConfig => fieldsetConfig
                                            .AddField(x => x.Author).MakeRequired()
                                            .AddField(x => x.Year).MakeRequired()
                                            .AddField(x => x.Image))));
                    });
            });
        });
        
}
