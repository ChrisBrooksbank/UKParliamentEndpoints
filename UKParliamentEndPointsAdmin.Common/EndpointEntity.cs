using Azure.Data.Tables;
using Azure;

namespace UKParliamentEndPointsAdmin.Shared
{
    public class EndPointEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string? Uri { get; set; }
        public string? Description { get; set; }

        public EndPointEntity()
        {
            PartitionKey = "endpoint";
        }
    }
}
