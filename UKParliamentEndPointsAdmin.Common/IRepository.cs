namespace UKParliamentEndPointsAdmin.Shared;
public interface IRepository
{
    Task<IEnumerable<EndPointEntity>> SearchAsync(SearchQuery searchQuery);
    Task<EndPointEntity> GetAsync(string id);
    Task AddAsync(EndPointEntity entity);
    Task UpdateAsync(EndPointEntity entity);
    Task DeleteAsync(string id);
    Task SetPingResponse(string id, int pingHttpResponseStatus, string pingStatus);
}