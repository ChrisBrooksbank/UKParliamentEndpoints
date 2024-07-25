using System.ComponentModel;

namespace UKParliamentEndPointsAdmin.Shared;
public class SearchQuery
{
    [Description("Skip")]
    public int? Skip { get; set; }
    [Description("Take")]
    public int? Take { get; set; }
    public bool? NewOrFailed { get; set; }
    public string? Description { get; set; }
    [Description("PartitionKey")]
    public string?  PartitionKey { get; set; }
    [Description("PingHttpResponseStatus")]
    public int? PingHttpResponseStatus { get; set; }
    [Description("PingStatus")]
    public int? PingStatus{ get; set; }
    [Description("DescriptionContains (slow clientside filter)")]
    public string? DescriptionContains { get; set; }
    [Description("UriContains (slow clientside filter)")]
    public string? UriContains { get; set; }
}