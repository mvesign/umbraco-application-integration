using Umbraco.Cms.Core;
using Umbraco.UIBuilder.Configuration.Builders;
using UmbracoApplicationIntegration.Logic.Services;
using UmbracoApplicationIntegration.Models;

namespace UmbracoApplicationIntegration.Logic.Configurations;

public static class UIBuilderDashboardConfiguration
{
    public static void AddBooksDashboard(this UIBuilderConfigBuilder builder) =>
        builder.WithSection(Constants.Applications.Content, withSectionConfig =>
        {
            withSectionConfig.AddDashboard("Books", dashboardConfig =>
            {
                dashboardConfig.SetCollection<Book>(
                    x => x.Title,
                    "Book",
                    "Books",
                    "A collection of books",
                    "icon-book",
                    "icon-books",
                    collectionConfig =>
                    {
                        collectionConfig
                            .SetAlias("publishedBooks")
                            .SetNameProperty(x => x.Title)
                            .SetRepositoryType<BookRepository>()
                            .Editor(editorConfig => editorConfig
                                .AddTab(
                                    "General",
                                    tabConfig => tabConfig
                                        .AddFieldset(
                                            "General",
                                            fieldsetConfig => fieldsetConfig
                                                .AddField(x => x.Title).SetLabel("Book Title")
                                                .AddField(x => x.Author).SetLabel("Book Author").SetDataType("Richtext editor")
                                                .AddField(x => x.Year).SetLabel("Year Published").SetDataType("Richtext editor"))));
                    });
            });
        });
}
