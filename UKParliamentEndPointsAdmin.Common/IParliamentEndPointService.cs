namespace UKParliamentEndPointsAdmin.Shared
{
    public interface IParliamentEndPointService
    {
        Task<IEnumerable<ParliamentEndPoint>> GetAllAsync();
        Task AddAsync(ParliamentEndPoint endpoint);
    }
}
