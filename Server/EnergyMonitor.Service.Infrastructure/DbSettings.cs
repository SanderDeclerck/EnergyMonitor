namespace EnergyMonitor.Service.Infrastructure
{
    public interface IDbSettings
    {
        string MongoConnectionString { get; set; }
        string MongoDbName { get; set; }
        string MongoReadingsCollectionName { get; set; }
        string MongoMetersCollectionName { get; set; }
    }

    public class DbSettings : IDbSettings
    {
        public string MongoConnectionString { get; set; }
        public string MongoDbName { get; set; }
        public string MongoReadingsCollectionName { get; set; }
        public string MongoMetersCollectionName { get; set; }
    }
}
