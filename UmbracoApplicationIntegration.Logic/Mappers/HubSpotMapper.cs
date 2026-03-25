using Umbraco.Cms.Core.Mapping;
using UmbracoApplicationIntegration.Models.External.HubSpot;

namespace UmbracoApplicationIntegration.Logic.Mappers;

public sealed class HubSpotMapper : IMapDefinition
{
    public void DefineMaps(IUmbracoMapper mapper)
    {
        mapper.Define<HubSpotFormSubmission, HubSpotPayload>((source, context) => new HubSpotPayload(), MapHubSpotFormSubmissionToHubSpotPayload);
    }

    private static void MapHubSpotFormSubmissionToHubSpotPayload(HubSpotFormSubmission source, HubSpotPayload target, MapperContext context)
    {
        target.Fields = source.Fields;
        target.Context = new HubSpotPayloadContext
        {
            PageName = source.PageName,
            PageUri = source.PageUri
        };
    }
}
