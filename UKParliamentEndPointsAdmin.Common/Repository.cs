using Azure.Data.Tables;
using Microsoft.Extensions.Options;

namespace UKParliamentEndPointsAdmin.Shared
{
    public class Repository: IRepository
    {
        private readonly TableClient _endpointTableClient;
        private const string EndpointsTableName = "ukparliamentpublicendpoints";

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

            await foreach (var page in queryResults.AsPages())
            {
                allEntities.AddRange(page.Values);
            }

            return allEntities;
        }

        public async Task<EndPointEntity> GetAsync(string id)
        {
            var partitionKey = id.GetPartitionKey();
            var rowKey = id.GetRowKey();
            var queryResults = _endpointTableClient
                .QueryAsync<EndPointEntity>(e => e.PartitionKey == partitionKey && e.RowKey == rowKey );
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

        public async Task DeleteAsync(string id)
        {
            await _endpointTableClient.DeleteEntityAsync(id.GetPartitionKey(), id.GetRowKey());
        }

        public async Task SetCachedResponse(string id, string response)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                entity.CachedResponse = response;
                entity.CachedTimeStamp = DateTime.Now.ToUniversalTime();
                await _endpointTableClient.UpdateEntityAsync<EndPointEntity>(entity, entity.ETag);
            }
        }
    }
}
