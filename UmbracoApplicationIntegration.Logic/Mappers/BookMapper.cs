using System.Globalization;
using Umbraco.Cms.Core.Mapping;
using UmbracoApplicationIntegration.Models;
using UmbracoApplicationIntegration.Models.External;

namespace UmbracoApplicationIntegration.Logic.Mappers;

public class BookMapper : IMapDefinition
{
    private static readonly CultureInfo dutchProvider = new("nl-NL");

    public void DefineMaps(IUmbracoMapper mapper)
    {
        mapper.Define<ITBookJson, Book>((source, context) => new Book(), MapITBookJsonToBook);
        mapper.Define<ClassicBookJson, Book>((source, context) => new Book(), MapClassicBookJsonToBook);
        mapper.Define<Book, ClassicBookJson>((source, context) => new ClassicBookJson(), MapBookToClassicBookJson);
    }

    private static void MapITBookJsonToBook(ITBookJson source, Book target, MapperContext context)
    {
        target.Id = source.Id;
        target.Title = source.Title ?? string.Empty;
        target.Author = source.Authors?.Count > 0 ? source.Authors[0] : string.Empty;
        target.Image = source.ThumbnailUrl ?? string.Empty;

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
        target.Title = source.Title ?? string.Empty;
        target.Author = source.Author ?? string.Empty;
        target.Image = !string.IsNullOrEmpty(source.ImageLink)
            ? $"/ClassicBookImages/{source.ImageLink.Replace("images/", string.Empty)}"
            : "/media/luffz20o/placeholder.png";
        target.Year = source.Year;
    }

    private static void MapBookToClassicBookJson(Book source, ClassicBookJson target, MapperContext context)
    {
        target.Id = source.Id;
        target.Title = source.Title;
        target.Author = source.Author;
        target.ImageLink = !string.IsNullOrEmpty(source.Image) && source.Image.StartsWith("/ClassicBookImages/")
            ? $"images/{source.Image.Replace("/ClassicBookImages/", string.Empty)}"
            : source.Image;
        target.Year = source.Year;
    }
}
