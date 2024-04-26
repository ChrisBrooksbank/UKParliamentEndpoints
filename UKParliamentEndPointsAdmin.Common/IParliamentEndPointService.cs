namespace UKParliamentEndPointsAdmin.Shared
{
    public interface IParliamentEndPointService
    {
        Task<IEnumerable<ParliamentEndPoint>> GetAllAsync();
    }
}
