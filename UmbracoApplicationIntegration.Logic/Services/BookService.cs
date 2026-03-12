using System.Text.Json;
using Umbraco.Cms.Core.Mapping;
using UmbracoApplicationIntegration.Models;
using UmbracoApplicationIntegration.Models.External;
using File = System.IO.File;

namespace UmbracoApplicationIntegration.Logic.Services;

public sealed class BookService(IUmbracoMapper mapper)
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
        
        return booksJson != null && booksJson.Count > 0
            ? [.. mapper.MapEnumerable<T, Book>(booksJson)]
            : [];
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
}
