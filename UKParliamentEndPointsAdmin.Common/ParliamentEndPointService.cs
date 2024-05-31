namespace UKParliamentEndPointsAdmin.Shared
{
    public class ParliamentEndPointService : IParliamentEndPointService
    {
        private readonly IRepository _repository;
        private const int HttpRequestTimeOutSeconds = 10;

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
                PingTimeStamp = e.PingTimeStamp,
                PingHttpResponseStatus = e.PingHttpResponseStatus,
                PingStatus = e.PingStatus
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
                Description = entity.Description,
                PingTimeStamp = entity.PingTimeStamp,
                PingHttpResponseStatus = entity.PingHttpResponseStatus,
                PingStatus =  entity.PingStatus
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
                PingTimeStamp = endpoint.PingTimeStamp,
                PingHttpResponseStatus = endpoint.PingHttpResponseStatus,
                PingStatus = endpoint.PingStatus
            };

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task Ping(string id)
        {
            var endpoint = await this.GetAsync(id);
            if (!string.IsNullOrWhiteSpace(endpoint?.Uri))
            {
                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(HttpRequestTimeOutSeconds);

                var pingStatus = string.Empty;
                var pingHttpResponseStatus = 0;

                try
                {
                    var response = await httpClient.GetAsync(endpoint.Uri);
                    pingHttpResponseStatus = (int)response.StatusCode;
                    pingStatus = response.IsSuccessStatusCode ? "Success" : "Failed";
                  
                }
                catch (HttpRequestException ex)
                {
                    pingStatus = $"Failed : {ex.Message}";
                }
                catch (TaskCanceledException ex)
                {
                    pingStatus = $"Failed : timed out. {ex.Message}";
                }
                catch (Exception ex)
                {
                    pingStatus = $"Failed. error. {ex.Message}";
                }

                await _repository.SetPingResponse(endpoint.Id, pingHttpResponseStatus, pingStatus);
            }
        }

    }
}