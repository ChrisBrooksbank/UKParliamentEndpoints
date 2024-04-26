
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
    }
}