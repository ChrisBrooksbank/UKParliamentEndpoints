namespace UKParliamentEndPointsAdmin.Shared;

public interface IEndPointMapper
{
    EndPointEntity Map(ParliamentEndPoint endpoint);
    ParliamentEndPoint Map(EndPointEntity entity);
}
