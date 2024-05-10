namespace UKParliamentEndPointsAdmin.Shared
{
    public interface IRepository
    {
        Task<IEnumerable<EndPointEntity>> GetAllAsync();
        Task AddAsync(EndPointEntity entity);
    }
}
