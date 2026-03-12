using Umbraco.UIBuilder.Extensions;
using UmbracoApplicationIntegration.Logic;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG

builder.Configuration.AddJsonFile("appsettings.Local.json", true, true);

#endif

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddUIBuilder(config => config.AddRepositoriesSection())
    .AddDeliveryApi()
    .AddComposers()
    .Build();

var app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
