namespace UKParliamentEndPointsAdmin.Shared
{
    public class ParliamentEndPointService : IParliamentEndPointService
    {
        private readonly IRepository _repository;

        public ParliamentEndPointService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ParliamentEndPoint>> GetAllAsync()
        {
            var endPointEntities =  await _repository.GetAllAsync();
            return endPointEntities.Select(e => new ParliamentEndPoint
            {
                Id = $"{e.PartitionKey}.{e.RowKey}",
                Uri = e.Uri,
                Description = e.Description,
                CachedResponse = e.CachedResponse,
                CachedDateTime = e.CachedTimeStamp
            });
        }

        public async Task<ParliamentEndPoint> GetAsync(string id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
            {
                return null;
            }

            return new ParliamentEndPoint
            {
                Id = $"{entity.PartitionKey}.{entity.RowKey}",
                Uri = entity.Uri,
                Description = entity.Description
            };
        }

        public async Task AddAsync(ParliamentEndPoint endpoint)
        {
            var entity = new EndPointEntity
            {
                PartitionKey = endpoint.Id.GetPartitionKey(),
                RowKey = endpoint.Id.GetRowKey(),
                Timestamp = DateTime.Now,
                Uri = endpoint.Uri,
                Description = endpoint.Description,
                CachedResponse = endpoint.CachedResponse,
                CachedTimeStamp = endpoint.CachedDateTime
            };

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task CacheResponse(string id)
        {
            var endpoint = await this.GetAsync(id);
            if (!string.IsNullOrWhiteSpace(endpoint?.Uri))
            {
                var responseString = await FetchStringFromEndpointAsync(endpoint.Uri);
                await _repository.SetCachedResponse(endpoint.Id, responseString);
            }
        }

        static async Task<string> FetchStringFromEndpointAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}