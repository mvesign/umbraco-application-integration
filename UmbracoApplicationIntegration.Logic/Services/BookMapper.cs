using System.Globalization;
using System.Text.Json;
using Umbraco.Cms.Core.Mapping;
using UmbracoApplicationIntegration.Models;
using UmbracoApplicationIntegration.Models.External;
using File = System.IO.File;

namespace UmbracoApplicationIntegration.Logic.Services;

public class BookMapper : IMapDefinition
{
    private static readonly CultureInfo dutchProvider = new("nl-NL");

    public void DefineMaps(IUmbracoMapper mapper)
    {
        mapper.Define<ITBookJson, Book>((source, context) => new Book(), MapITBookJsonToBook);
        mapper.Define<ClassicBookJson, Book>((source, context) => new Book(), MapClassicBookJsonToBook);
    }

    public List<ITBookJson> LoadITBooksFromJson(string filePath) =>
        LoadBooksFromJson<ITBookJson>(filePath);

    public List<ClassicBookJson> LoadClassicBooksFromJson(string filePath) =>
        LoadBooksFromJson<ClassicBookJson>(filePath);

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

    private static void MapITBookJsonToBook(ITBookJson source, Book target, MapperContext context)
    {
        target.Id = source.Id;
        target.Title = source.Title;
        target.Author = source.Authors?.Count > 0 ? source.Authors[0] : null;
        target.Image = source.ThumbnailUrl;

        if (DateTime.TryParse(source.PublishedDate?.Date, dutchProvider, out var publishedDate))
        {
            target.Year = publishedDate.Year;
        }
        else if (source.PublishedDate?.Date?.Length >= 4
            && int.TryParse(source.PublishedDate.Date.AsSpan(0, 4), out var year))
        {
            target.Year = year;
        }
    }

    private static void MapClassicBookJsonToBook(ClassicBookJson source, Book target, MapperContext context)
    {
        target.Id = source.Id;
        target.Title = source.Title;
        target.Author = source.Author;
        target.Image = !string.IsNullOrEmpty(source.ImageLink)
            ? $"/ClassicBookImages/{source.ImageLink.Replace("images/", "")}"
            : "/media/luffz20o/placeholder.png";
        target.Year = source.Year;
    }
}
