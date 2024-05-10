namespace UKParliamentEndPointsAdmin.Shared
{
    public interface IRepository
    {
        Task<IEnumerable<EndPointEntity>> GetAllAsync();
        Task<EndPointEntity> GetAsync(string id);
        Task AddAsync(EndPointEntity entity);
        Task DeleteAsync(string id);
        Task SetCachedResponse(string id, string response);
    }
}
