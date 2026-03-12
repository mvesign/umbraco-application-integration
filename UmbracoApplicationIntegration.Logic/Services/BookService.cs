using Umbraco.Cms.Core.Mapping;
using UmbracoApplicationIntegration.Models;

namespace UmbracoApplicationIntegration.Logic.Services;

public sealed class BookService(IUmbracoMapper mapper, BookMapper bookMapper)
{
    private const string JsonDirectory = "../Data/";

    public List<Book> GetITBooks() =>
        GetBooks("ITBooks.json", bookMapper.LoadITBooksFromJson);

    public List<Book> GetClassicBooks() =>
        GetBooks("ClassicBooks.json", bookMapper.LoadClassicBooksFromJson);

    private List<Book> GetBooks<T>(string fileName, Func<string, List<T>> loadBooksFromJson)
    {
        var filePath = Path.Combine(JsonDirectory, fileName);

        var booksJson = loadBooksFromJson(filePath);
        
        return booksJson?.Count > 0
            ? [.. mapper.MapEnumerable<T, Book>(booksJson!)]
            : [];
    }
}
