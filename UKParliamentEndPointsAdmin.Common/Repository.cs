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
            var tableServiceClient = new TableServiceClient(settings.Value.AzureTableConnectionString);
            tableServiceClient.CreateTableIfNotExists(EndpointsTableName);
            _endpointTableClient = tableServiceClient.GetTableClient(EndpointsTableName);
        }

        public async Task<IEnumerable<EndPointEntity>> GetAllAsync()
        {
            var queryResults = _endpointTableClient.QueryAsync<EndPointEntity>(); 
            List<EndPointEntity> allEntities = new List<EndPointEntity>();

            await foreach (var page in queryResults.AsPages())
            {
                allEntities.AddRange(page.Values);
            }

            return allEntities;
        }
           
    }
}
