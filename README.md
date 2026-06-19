# UKParliamentEndpoints

A .NET 8 API for maintaining a collection of public UK Parliament API endpoints.

Endpoint metadata is stored in Azure Table Storage so Parliament data sources can be discovered, checked, and reused more easily.

## Project structure

- `admin/` - ASP.NET Core API.
- `UKParliamentEndPointsAdmin.Common/` - shared models and Azure Table Storage integration.
- `.github/` - GitHub configuration and workflows.

## Endpoints

The API controller is routed under `ParliamentEndpoint`.

### `GET /ParliamentEndpoint/endpoints`

Retrieves the stored Parliament API endpoints.

### `GET /ParliamentEndpoint/endpoints/{id}`

Retrieves a specific endpoint by ID.

### `POST /ParliamentEndpoint/endpoints`

Adds a new Parliament endpoint.

### `PUT /ParliamentEndpoint/endpoints`

Updates an existing Parliament endpoint.

### `DELETE /ParliamentEndpoint/endpoints/{id}`

Deletes a Parliament endpoint by ID.

### `POST /ParliamentEndpoint/endpoints/{id}/ping`

Checks a specific endpoint and records the result.

## Configuration

Configure Azure Table Storage settings in `appsettings.json`, user secrets, or environment-specific configuration.

The API reads settings from the `AzureStorage` configuration section.

## Run locally

```bash
dotnet restore
dotnet run --project admin/UKParliamentEndPointsAdmin.API.csproj
```

Swagger is enabled by the application and can be used to explore the API locally.

## Related repositories

- [UKParliamentEndPointsAdmin](https://github.com/ChrisBrooksbank/UKParliamentEndPointsAdmin) - Blazor admin UI.
- [UKParlyEndPointsFuncApp](https://github.com/ChrisBrooksbank/UKParlyEndPointsFuncApp) - scheduled endpoint checks.
- [UKParliamentEndPointsAIChat](https://github.com/ChrisBrooksbank/UKParliamentEndPointsAIChat) - AI chat interface over the endpoint data.
