using System.Text.Json;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Web;
using UmbracoApplicationIntegration.Models;
using UmbracoApplicationIntegration.Models.External;
using File = System.IO.File;

namespace UmbracoApplicationIntegration.Logic.Services;

public sealed class BookService(
    IUmbracoMapper mapper,
    IUmbracoContextAccessor umbracoContextAccessor)
{
    private const string JsonDirectory = "../Data/";
    private const string ITBooksFileName = "ITBooks.json";
    private const string ClassicBooksFileName = "ClassicBooks.json";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public List<Book> GetITBooks() =>
        GetBooks(ITBooksFileName, LoadBooksFromJson<ITBookJson>);

    public List<Book> GetClassicBooks() =>
        GetBooks(ClassicBooksFileName, LoadBooksFromJson<ClassicBookJson>);

    public void WriteITBooks(List<Book> books) =>
        WriteBooksToJson<ITBookJson>(GetJsonFilePath(ITBooksFileName), books);

    public void WriteClassicBooks(List<Book> books) =>
        WriteBooksToJson<ClassicBookJson>(GetJsonFilePath(ClassicBooksFileName), books);

    private List<Book> GetBooks<T>(string fileName, Func<string, List<T>> loadBooksFromJson)
    {
        var filePath = GetJsonFilePath(fileName);

        var booksJson = loadBooksFromJson(filePath);
        
        if (booksJson == null || booksJson.Count == 0)
        {
            return [];
        }

        var books = mapper.MapEnumerable<T, Book>(booksJson).ToList();
        foreach (var book in books)
        {
            book.Reviews = GetReviewsByBookId(book.Id);
        }
        return books;
    }

    private static string GetJsonFilePath(string fileName) =>
        Path.Combine(JsonDirectory, fileName);

    private static List<T> LoadBooksFromJson<T>(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return [];
            }

            var content = File.ReadAllText(filePath);

            return !string.IsNullOrWhiteSpace(content)
                ? JsonSerializer.Deserialize<List<T>>(content) ?? []
                : [];
        }
        catch
        {
            // Don't really cares why, just return an empty list if something goes wrong.
            return [];
        }
    }

    private void WriteBooksToJson<T>(string filePath, List<Book> books)
    {
        try
        {
            if (books == null || !books.Any())
            {
                return;
            }

            if (!File.Exists(filePath))
            {
                return;
            }

            var mappedBooks = mapper.MapEnumerable<Book, T>(books);
            if (mappedBooks == null || mappedBooks.Count == 0)
            {
                return;
            }

            var content = JsonSerializer.Serialize(mappedBooks, JsonSerializerOptions);
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            File.WriteAllText(filePath, content);
        }
        catch
        {
            // Don't really cares why.
        }
    }

    private List<Review> GetReviewsByBookId(int bookId)
    {
        if (!umbracoContextAccessor.TryGetUmbracoContext(out var context)
            || context is null
            || !(context.PublishedRequest?.PublishedContent?.Root() is var rootNode)
            || rootNode is null)
        {
            return [];
        }

        var reviewsNode = rootNode.FirstChildOfType(ClassicBooks.ModelTypeAlias);

        return reviewsNode?
            .ChildrenOfType(ClassicBookReview.ModelTypeAlias)
            .Where(x => x.Value<int>("bookId") == bookId)
            .Select(x => new Review
            {
                Content = x.Value<string>("content") ?? string.Empty,
                Reviewer = x.Value<string>("reviewer") ?? string.Empty,
            })
            .ToList()
            ?? [];
    }
}
