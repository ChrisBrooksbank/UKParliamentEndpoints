namespace UKParliamentEndPointsAdmin.Shared
{
    public interface IParliamentEndPointService
    {
        Task<IEnumerable<ParliamentEndPoint>> GetAllAsync();
        Task<ParliamentEndPoint> GetAsync(string id);
        Task AddAsync(ParliamentEndPoint endpoint);
        Task DeleteAsync(string id);
        Task Ping(string id);
    }
}
