namespace UKParliamentEndPointsAdmin.Shared
{
    public class ParliamentEndPoint
    {
        public string Id { get; set; }

        public string? Uri { get; set; }

        public string? Description { get; set; }
        public string CachedResponse { get; set; }
        public DateTime CachedDateTime { get; set; }
    }
}
