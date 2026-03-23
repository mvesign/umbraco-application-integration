using Examine.Lucene;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;

namespace UmbracoApplicationIntegration.Logic.Indexing;

public sealed class ITBooksIndex(
    ILoggerFactory loggerFactory,
    string name,
    IOptionsMonitor<LuceneDirectoryIndexOptions> indexOptions,
    IHostingEnvironment hostingEnvironment,
    IRuntimeState runtimeState) : UmbracoExamineIndex(loggerFactory, name, indexOptions, hostingEnvironment, runtimeState)
{
    public const string IndexName = "ITBookIndex";
    public const string CategoryName = "itbook";
}
