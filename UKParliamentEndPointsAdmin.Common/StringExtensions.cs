namespace UKParliamentEndPointsAdmin.Shared
{
    public static class StringExtensions
    {
        public static string GetPartitionKey(this string id)
        {
            int dotPosition = id.IndexOf(".");
            if (dotPosition < 0)
            {
                return string.Empty;
            }

            return id.Substring(0, dotPosition);
        }

        public static string GetRowKey(this string id)
        {
            int dotPosition = id.IndexOf(".");
            if (dotPosition < 0)
            {
                return id;
            }

            return id.Substring(dotPosition + 1);
        }
    }
}
