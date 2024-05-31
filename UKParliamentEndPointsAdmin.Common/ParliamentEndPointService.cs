namespace UKParliamentEndPointsAdmin.Shared
{
    public class ParliamentEndPointService : IParliamentEndPointService
    {
        private readonly IRepository _repository;
        private readonly IEndPointMapper _mapper;
        private const int HttpRequestTimeOutSeconds = 10;

        public ParliamentEndPointService(IRepository repository, IEndPointMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParliamentEndPoint>> SearchAsync(SearchQuery searchQuery)
        {
            var endPointEntities =  await _repository.SearchAsync(searchQuery);
            return endPointEntities.Select(_mapper.Map);
        }

        public async Task<ParliamentEndPoint> GetAsync(string id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
            {
                return null;
            }
            return _mapper.Map(entity);
        }

        public async Task<ParliamentEndPoint> AddAsync(ParliamentEndPoint endpoint)
        {
            var entity = _mapper.Map(endpoint);
            await _repository.AddAsync(entity);
            return await GetAsync(endpoint.Id);
        }

        public async Task<ParliamentEndPoint> UpdateAsync(ParliamentEndPoint endpoint)
        {
            var entityOnDb = await _repository.GetAsync(endpoint.Id);
            var entity = _mapper.Map(endpoint);
            entity.ETag = entityOnDb.ETag;
            await _repository.UpdateAsync(entity);
            await Ping(endpoint.Id);
            return _mapper.Map(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ParliamentEndPoint> Ping(string id)
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
                    var request = new HttpRequestMessage(HttpMethod.Head, endpoint.Uri);
                    var response = await httpClient.SendAsync(request);
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
            return await GetAsync(id);
        }
    }
}