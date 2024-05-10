namespace UKParliamentEndPointsAdmin.Shared
{
    public class AzureStorageSettings
    {
        public string AzureTableConnectionString => Environment.GetEnvironmentVariable("UKParliamentEndPoints.ConnectionString");
    }
}
