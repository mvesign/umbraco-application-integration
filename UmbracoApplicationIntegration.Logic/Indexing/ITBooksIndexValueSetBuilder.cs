using Examine;
using Umbraco.Cms.Infrastructure.Examine;
using UmbracoApplicationIntegration.Models.Indexing;

namespace UmbracoApplicationIntegration.Logic.Indexing;

public sealed class ITBooksIndexValueSetBuilder : IValueSetBuilder<ITBookModel>
{
    public IEnumerable<ValueSet> GetValueSets(params ITBookModel[] content) =>
        content.Select(MapToValueSet);

    private static ValueSet MapToValueSet(ITBookModel book) =>
        new(
            book.ObjectID,
            ITBooksIndex.CategoryName,
            new Dictionary<string, object>
            {
                [nameof(ITBookModel.ObjectID)] = book.ObjectID,
                [nameof(ITBookModel.Title)] = book.Title,
                [nameof(ITBookModel.PageCount)] = book.PageCount,
                [nameof(ITBookModel.PublishedDate)] = MapToPublishedDate(book.PublishedDate),
                [nameof(ITBookModel.ThumbnailUrl)] = book.ThumbnailUrl,
                [nameof(ITBookModel.ShortDescription)] = book.ShortDescription,
                [nameof(ITBookModel.LongDescription)] = book.LongDescription,
                [nameof(ITBookModel.Authors)] = MapToJoinedArray(book.Authors),
                [nameof(ITBookModel.Categories)] = MapToJoinedArray(book.Categories)
            });

    private static string MapToPublishedDate(IDictionary<string, object>? publishedDate)
    {
        if (publishedDate == null)
        {
            return string.Empty;
        }

        var result = publishedDate.TryGetValue("$date", out var rawDateValue)
            && rawDateValue is string dateValue
                ? dateValue
                : publishedDate.ToString();

        return result.IfNullOrWhiteSpace(string.Empty);
    }

    private static string MapToJoinedArray(IEnumerable<string>? values) =>
        values != null
            ? string.Join(", ", values)
            : string.Empty;
}
