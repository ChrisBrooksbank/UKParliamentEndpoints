namespace UKParliamentEndPointsAdmin.Shared;

public class EndPointMapper : IEndPointMapper
{
    public EndPointEntity Map(ParliamentEndPoint endpoint)
    {
        var entity = new EndPointEntity
        {
            PartitionKey = endpoint.Id.GetPartitionKey(),
            RowKey = endpoint.Id.GetRowKey(),
            Timestamp = DateTime.Now,
            Uri = endpoint.Uri,
            Description = endpoint.Description,
            PingTimeStamp = endpoint.PingTimeStamp,
            PingHttpResponseStatus = endpoint.PingHttpResponseStatus,
            PingStatus = endpoint.PingStatus
        };
        return entity;
    }

    public ParliamentEndPoint Map(EndPointEntity entity)
    {
        return new ParliamentEndPoint
        {
            Id = $"{entity.PartitionKey}.{entity.RowKey}",
            Uri = entity.Uri,
            Description = entity.Description,
            PingTimeStamp = entity.PingTimeStamp,
            PingHttpResponseStatus = entity.PingHttpResponseStatus,
            PingStatus = entity.PingStatus
        };
    }
}