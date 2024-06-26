﻿namespace UKParliamentEndPointsAdmin.Shared;
public interface IParliamentEndPointService
{
    Task<IEnumerable<ParliamentEndPoint>> SearchAsync(SearchQuery searchQuery);
    Task<ParliamentEndPoint> GetAsync(string id);
    Task<ParliamentEndPoint> AddAsync(ParliamentEndPoint endpoint);
    Task<ParliamentEndPoint> UpdateAsync(ParliamentEndPoint endpoint);
    Task DeleteAsync(string id);
    Task<ParliamentEndPoint> Ping(string id);
}