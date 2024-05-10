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
                Description = e.Description
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
                Description = endpoint.Description
            };

            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}