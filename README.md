# Umbraco Application Integration

A training project demonstrating how to integrate external services and custom backoffice tooling into an Umbraco CMS solution.

## Overview

This repository contains practical examples for working with common integration scenarios in Umbraco. It combines frontend rendering, backoffice extensions, external API communication, and custom indexing into a single solution featuring:

- **Umbraco CMS 17.2.2** - The main CMS platform for the website and backoffice.
- **.NET 10.0** - The target framework for all projects in the solution.
- **Umbraco UI Builder 17.1.0** - For custom repository-driven backoffice sections and dashboards.
- **uSync 17.0.4** - For synchronizing Umbraco settings and supporting development workflows.
- **Umbraco Integrations for HubSpot 8.0.0** - For CRM form integration and submission flows.
- **Umbraco Integrations for Algolia 6.0.0** - For search configuration and indexed IT book content.

The solution is split into three projects:

- **UmbracoApplicationIntegration.Website** - The runnable Umbraco site and API endpoints.
- **UmbracoApplicationIntegration.Logic** - Integration logic, services, composers, mappings, indexing, and UI Builder configuration.
- **UmbracoApplicationIntegration.Models** - Shared domain models, generated content models, and external DTOs.

## Features

### Integration Examples

- **HubSpot form integration**
	- Fetches form metadata from HubSpot through a custom API controller.
	- Submits form responses back to HubSpot.
	- Creates Umbraco review content after a successful form submission.

- **Algolia-backed IT books search**
	- Reads IT book data from Algolia.
	- Populates a custom Examine index for search scenarios.
	- Exposes searchable content alongside classic book data on the homepage.

- **JSON-based classic book repository**
	- Uses local JSON files in the `Data` folder as a lightweight data source.
	- Maps external book data into shared models.
	- Persists updates back to JSON through a custom repository.

### Backoffice Components

- **UI Builder section**
	- Adds a custom `Repositories` section.
	- Manages books through a repository-backed collection.
	- Includes filters, searchable fields, custom actions, and permission-based editing.

- **UI Builder dashboard**
	- Adds a custom `Books` dashboard in the Content section.
	- Demonstrates editor configuration for repository-based entities.

- **Custom indexing pipeline**
	- Registers a dedicated `ITBookIndex` Examine index.
	- Maps Algolia records into Umbraco-friendly value sets.

- **uSync support**
	- Includes synchronized Umbraco settings under the `uSync` folder.

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later.
- A code editor such as Visual Studio 2026, VS Code, or Rider.
- SQL Server or SQL LocalDB for the Umbraco database.
- An Algolia account with an index containing IT book data.
- A HubSpot account and form configuration for the CRM integration examples.

### Initial setup

1. **Clone the repository**
	 ```bash
	 git clone https://github.com/mvesign/umbraco-application-integration.git
	 cd umbraco-application-integration
	 ```

2. **Restore .NET dependencies**
	 ```bash
	 dotnet restore
	 ```

3. **Configure local appsettings**
	 - Copy `UmbracoApplicationIntegration.Website/appsettings.json` to `UmbracoApplicationIntegration.Website/appsettings.Local.json`.
	 - Set a valid database connection string.
	 - Add your Algolia settings:
		 - `Umbraco:CMS:Integrations:Search:Algolia:Settings:ApplicationId`
		 - `Umbraco:CMS:Integrations:Search:Algolia:Settings:AdminApiKey`
		 - `Umbraco:CMS:Integrations:Search:Algolia:Settings:SearchApiKey`
		 - `Umbraco:CMS:Integrations:Search:Algolia:Settings:IndexName`
	 - Add your HubSpot settings:
		 - `Umbraco:Integrations:Crm:Hubspot:Settings:ApiKey`
		 - `Umbraco:Integrations:Crm:Hubspot:Settings:PortalId`
		 - `Umbraco:Integrations:Crm:Hubspot:Settings:FormId`
		 - `Umbraco:Integrations:Crm:Hubspot:Settings:Region`
	 - If you want to use the authorized services flow, also configure:
		 - `Umbraco:AuthorizedServices:Services:hubspot:ClientId`
		 - `Umbraco:AuthorizedServices:Services:hubspot:ClientSecret`

4. **Setup a local database**
	 - Create a new empty SQL Server or SQL LocalDB database.
	 - Update `ConnectionStrings:umbracoDbDSN` in `appsettings.Local.json`.

5. **Run the website project**
	 ```bash
	 cd UmbracoApplicationIntegration.Website
	 dotnet run
	 ```

6. **Access the site**
	 - Navigate to `https://localhost:XXXX` using the URL shown in the console.
	 - Complete the Umbraco installation if your local setup is not already initialized.
	 - Open the backoffice at `/umbraco`.

## Development Configuration

The project uses local configuration overrides for development:

- `appsettings.Local.json` is loaded in `DEBUG` mode.
- The website project uses source-generated models stored in `UmbracoApplicationIntegration.Models/Content/Generated`.
- Sample JSON data for the repository examples lives in the top-level `Data` folder.
- uSync files are stored under `UmbracoApplicationIntegration.Website/uSync`.

## License

See [LICENSE](LICENSE) for details.