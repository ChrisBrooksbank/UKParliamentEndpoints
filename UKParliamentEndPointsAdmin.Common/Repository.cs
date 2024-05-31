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

    public async Task<IEnumerable<EndPointEntity>> GetAllAsync()
    {
        var queryResults = _endpointTableClient.QueryAsync<EndPointEntity>();
        var allEntities = new List<EndPointEntity>();
        var pageCount = 0;

        await foreach (var page in queryResults.AsPages())
        {
            allEntities.AddRange(page.Values);
            pageCount += page.Values.Count;

            if (pageCount >= MaxPageSize)
            {
                break;
            }
        }

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