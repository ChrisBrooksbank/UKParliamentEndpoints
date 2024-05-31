namespace UKParliamentEndPointsAdmin.Shared;
public class ParliamentEndPoint
{
    public string Id { get; set; }

    public string? Uri { get; set; }

    public string? Description { get; set; }
    public DateTime PingTimeStamp { get; set; }
    public int PingHttpResponseStatus { get; set; }
    public string? PingStatus { get; set; }
}