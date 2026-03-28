using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Umbraco.AuthorizedServices.Services;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Services;
using UmbracoApplicationIntegration.Logic.Settings;
using UmbracoApplicationIntegration.Models.External.HubSpot;

namespace UmbracoApplicationIntegration.Website.Controllers;

[Route("api/hubspot")]
public class HubSpotController(
    IOptions<HubSpotClientSettings> hubSpotClientSettings,
    IHttpClientFactory httpClientFactory,
    IUmbracoMapper mapper,
    IContentService contentService,
    IAuthorizedServiceCaller authorizedServiceCaller) : Controller
{
    private readonly HubSpotClientSettings _hubSpotClientSettings = hubSpotClientSettings.Value;

    [HttpGet("hubspot-form")]
    public async Task<IActionResult> GetHubSpotForm()
    {
        var hubSpotUrl = $"https://api.hubapi.com/marketing/v3/forms/{_hubSpotClientSettings.FormId}";

        try
        {
            using var httpClient = CreateHttpClient();

            var response = await httpClient.GetAsync(hubSpotUrl);

            response = response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, new { message = "Error fetching data from HubSpot", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
        }
    }

    [HttpPost("hubspot-form-submit")]
    public async Task<IActionResult> SubmitHubSpotForm([FromBody] HubSpotFormSubmission formSubmission)
    {
        var hubSpotUrl = $"https://api.hsforms.com/submissions/v3/integration/submit/{_hubSpotClientSettings.PortalId}/{_hubSpotClientSettings.FormId}";

        try
        {
            var hubSpotPayload = mapper.Map<HubSpotPayload>(formSubmission);
            if (hubSpotPayload is null)
            {
                return BadRequest();
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(hubSpotPayload), Encoding.UTF8, "application/json");

            using var httpClient = CreateHttpClient();

            var response = await httpClient.PostAsync(hubSpotUrl, jsonContent);

            response = response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, new { message = "Error submitting data to HubSpot", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred", details = ex.Message });
        }
    }

    [HttpPost("create-book-review")]
    public IActionResult CreateBookReview([FromBody] HubSpotFormSubmission formSubmission)
    {
        // TODO: Now hardcoded, but can be determined a bit better
        var parentId = new Guid("e88aeac0-e7ac-4255-89fd-7e39865a66d5"); 

        try
        {
            var title = formSubmission.Fields.FirstOrDefault(f => f.Name == "book_title")?.Value;
            
            var newBookReview = contentService.Create("New Review: " + title, parentId, "bookReview");

            newBookReview.SetValue("title", title);
            newBookReview.SetValue("author", formSubmission.Fields.FirstOrDefault(f => f.Name == "author")?.Value);
            newBookReview.SetValue("review", formSubmission.Fields.FirstOrDefault(f => f.Name == "book_review")?.Value);

            var saveResult = contentService.Save(newBookReview, -1);
            if (!saveResult.Success)
            {
                return StatusCode(500, new { message = "Error creating book review" });
            }

            return Ok(new { message = "Book review created successfully", bookReviewId = newBookReview.Id });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating book review", details = ex.Message });
        }
    }

    private async Task<HttpClient> CreateHttpClient()
    {
        var httpClient = httpClientFactory.CreateClient();

        var accessToken = await GetAccessTokenAsync();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return httpClient;
    }

    private async Task<string> GetAccessTokenAsync()
    {
        // Use the GetOAuth2AccessToken method to fetch the access token
        var tokenResult = await authorizedServiceCaller.GetOAuth2AccessToken(HubSpotClientSettings.ServiceAlias);
        return tokenResult.Success && !string.IsNullOrWhiteSpace(tokenResult.Result)
            ? tokenResult.Result
            : _hubSpotClientSettings.ApiKey;
    }
}
