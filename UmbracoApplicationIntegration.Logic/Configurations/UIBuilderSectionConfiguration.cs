using Umbraco.UIBuilder.Configuration.Builders;
using Umbraco.UIBuilder.Configuration.Collections;
using Umbraco.UIBuilder.Infrastructure.Configuration.Actions;
using UmbracoApplicationIntegration.Logic.Actions;
using UmbracoApplicationIntegration.Logic.Services;
using UmbracoApplicationIntegration.Models;

namespace UmbracoApplicationIntegration.Logic.Configurations;

public static class UIBuilderSectionConfiguration
{
    private static readonly string[] AllowedUserGroupAliases = [ "admin", "editor" ];

    public static void AddRepositoriesSection(this UIBuilderConfigBuilder builder, bool readOnly = false) =>
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
                            // Set the icon color to blue for each item within the collection view.
                            .SetIconColor("blue")
                            .SetNameProperty(x => x.Title)
                            .ListView(listViewConfig =>
                                listViewConfig
                                    .AddField(x => x.Author).SetHeading("Author")
                                    .AddField(x => x.Year).SetHeading("Year Published")
                                    .AddField(x => x.Image).SetHeading("Image")
                                    // Set page size to enable pagination.
                                    .SetPageSize(10))
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
                                            // Add validation to the field Year to ensure it falls between -3000 and 3000.
                                            .AddField(x => x.Year).MakeRequired().SetValidationRegex("^-?(3000|[0-2]?[0-9]{1,3})$")
                                            .AddField(x => x.Image))));

                        // Check if we need to make the collection a readonly collection for every user group.
                        if (readOnly)
                        {
                            collectionConfig
                                .DisableCreate()
                                .DisableDelete()
                                .MakeReadOnly();
                        }
                        // Otherwise only allow certain user groups are allowed to create new items within the collection.
                        else
                        {
                            collectionConfig
                                .DisableCreate(collectionVisibilityContext => !collectionVisibilityContext.IsAllowed())
                                .DisableDelete(collectionVisibilityContext => !collectionVisibilityContext.IsAllowed())
                                .MakeReadOnly(collectionVisibilityContext => !collectionVisibilityContext.IsAllowed());
                        }

                        // Add quick filter cards.
                        collectionConfig
                            .AddCard("Books published before 1850", x => x.Year < 1850)
                            .AddCard("Books published After 1850", x => x.Year >= 1850);

                        // Add searchable properties.
                        collectionConfig
                            .AddSearchableProperty(x => x.Author)
                            .AddSearchableProperty(x => x.Title);

                        // Add filterable properties.
                        collectionConfig
                            .AddFilterableProperty(x => x.Year);

                        // Add custom actions to appear on the "three dots" actions column.
                        collectionConfig
                            .AddAction<SomeCustomAction>();
                    });
            });
        });

    private static bool IsAllowed(this CollectionPermissionContext context) =>
        context.UserGroups.Any(x => AllowedUserGroupAliases.Contains(x.Alias));
}
