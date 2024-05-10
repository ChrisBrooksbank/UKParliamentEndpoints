
using static System.Net.Mime.MediaTypeNames;

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

        public async Task AddAsync(ParliamentEndPoint endpoint)
        {
            int dotIndex = endpoint.Id.IndexOf('.');
            if (dotIndex < 0)
            {
                throw new ArgumentException("ID needs to split with a dot");
            }

            var entity = new EndPointEntity
            {
                PartitionKey = endpoint.Id.Substring(0, dotIndex),
                RowKey = endpoint.Id.Substring(dotIndex + 1),
                Timestamp = DateTime.Now,
                Uri = endpoint.Uri,
                Description = endpoint.Description
            };

            await _repository.AddAsync(entity);
        }
    }
}