using Azure.Data.Tables;
using Microsoft.Extensions.Options;

namespace UKParliamentEndPointsAdmin.Shared;
public class Repository : IRepository
{
    private readonly TableClient _endpointTableClient;
    private const string EndpointsTableName = "ukparliamentpublicendpoints";
    private const int MaxPageSize = 200;

    public Repository(IOptions<AzureStorageSettings> settings)
    {
        var connectionString = settings.Value.AzureTableConnectionString;
        var tableServiceClient = new TableServiceClient(connectionString);
        tableServiceClient.CreateTableIfNotExists(EndpointsTableName);
        _endpointTableClient = tableServiceClient.GetTableClient(EndpointsTableName);
    }

    public async Task<IEnumerable<EndPointEntity>> SearchAsync(SearchQuery searchQuery)
    {
        var query = _endpointTableClient.QueryAsync<EndPointEntity>();
        
        var queryResults = query.AsPages();
        var allEntities = new List<EndPointEntity>();

        await foreach (var page in queryResults)
        {
            allEntities.AddRange(page.Values);
        }

        if (!string.IsNullOrWhiteSpace(searchQuery.Description))
        {
            allEntities = allEntities
                .Where(e => e.Description?.Contains(searchQuery.Description, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }

        if (searchQuery.Skip is > 0)
        {
            allEntities = allEntities.Skip(searchQuery.Skip.Value).ToList();
        }
        allEntities = allEntities.Take(searchQuery.Take ?? MaxPageSize).ToList();
        return allEntities;
    }

    public async Task<EndPointEntity> GetAsync(string id)
    {
        var partitionKey = id.GetPartitionKey();
        var rowKey = id.GetRowKey();
        var queryResults = _endpointTableClient
            .QueryAsync<EndPointEntity>(e => e.PartitionKey == partitionKey && e.RowKey == rowKey);
        var allEntities = new List<EndPointEntity>();

        await foreach (var page in queryResults.AsPages())
        {
            allEntities.AddRange(page.Values);
        }

        return allEntities.SingleOrDefault();
    }

    public async Task AddAsync(EndPointEntity entity)
    {
        await _endpointTableClient.AddEntityAsync<EndPointEntity>(entity);
    }

    public async Task UpdateAsync(EndPointEntity entity)
    {
        await _endpointTableClient.UpdateEntityAsync<EndPointEntity>(entity, entity.ETag, TableUpdateMode.Replace);
    }

    public async Task DeleteAsync(string id)
    {
        await _endpointTableClient.DeleteEntityAsync(id.GetPartitionKey(), id.GetRowKey());
    }

    public async Task SetPingResponse(string id, int pingHttpResponseStatus, string pingStatus)
    {
        var entity = await GetAsync(id);
        if (entity != null)
        {
            entity.PingTimeStamp = DateTime.Now.ToUniversalTime();
            entity.PingHttpResponseStatus = pingHttpResponseStatus;
            entity.PingStatus = pingStatus;
            await _endpointTableClient.UpdateEntityAsync<EndPointEntity>(entity, entity.ETag);
        }
    }
}