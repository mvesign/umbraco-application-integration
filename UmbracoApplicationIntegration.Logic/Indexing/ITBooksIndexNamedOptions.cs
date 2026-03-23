using Examine;
using Examine.Lucene;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using UmbracoApplicationIntegration.Models.Indexing;

namespace UmbracoApplicationIntegration.Logic.Indexing;

public sealed class ITBooksIndexNamedOptions(
    IOptions<IndexCreatorSettings> settings) : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
{
    public void Configure(string? name, LuceneDirectoryIndexOptions options)
    {
        if (name is null || !string.Equals(name, ITBooksIndex.IndexName))
        {
            return;
        }

        options.Analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
        options.UnlockIndex = true;
        options.FieldDefinitions = new FieldDefinitionCollection(
            new FieldDefinition(nameof(ITBookModel.ObjectID), FieldDefinitionTypes.FullTextSortable),
            new FieldDefinition(nameof(ITBookModel.Title), FieldDefinitionTypes.FullTextSortable),
            new FieldDefinition(nameof(ITBookModel.PageCount), FieldDefinitionTypes.Integer),
            new FieldDefinition(nameof(ITBookModel.PublishedDate), FieldDefinitionTypes.DateTime),
            new FieldDefinition(nameof(ITBookModel.ThumbnailUrl), FieldDefinitionTypes.FullTextSortable),
            new FieldDefinition(nameof(ITBookModel.ShortDescription), FieldDefinitionTypes.FullTextSortable),
            new FieldDefinition(nameof(ITBookModel.LongDescription), FieldDefinitionTypes.FullTextSortable),
            new FieldDefinition(nameof(ITBookModel.Authors), FieldDefinitionTypes.FullTextSortable),
            new FieldDefinition(nameof(ITBookModel.Categories), FieldDefinitionTypes.FullTextSortable)
        );

        if (settings.Value.LuceneDirectoryFactory == LuceneDirectoryFactory.SyncedTempFileSystemDirectoryFactory)
        {
            options.IndexDeletionPolicy = new SnapshotDeletionPolicy(
                new KeepOnlyLastCommitDeletionPolicy());
        }
    }

    public void Configure(LuceneDirectoryIndexOptions options)
    {
        // This is never called and is just part of the interface.
    }
}
