namespace FleetManager.DataAccessLayer.Tests.DataContextBuilders
{
    static class DataContextBuilderExtensions
    {
        public static SqlServerDataContextBuilder SqlServer(this SupportedPlatforms platforms, string connectionString)
        {
            return new SqlServerDataContextBuilder(connectionString);
        }

        public static MemoryDataContextBuilder Memory(this SupportedPlatforms platforms)
        {
            return new MemoryDataContextBuilder();
        }
    }

}
