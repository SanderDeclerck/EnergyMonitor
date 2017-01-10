using EnergyMonitor.Service.Infrastructure;
using MongoDB.Driver;

namespace EnergyMonitor.Service.Dao
{
    public interface IEnergyDbContext
    {
        IMongoCollection<EnergyReadingMongoModel> ReadingCollection { get; }
        IMongoCollection<MeterMongoModel> MeterCollection { get; }
    }

    public class EnergyDbContext : IEnergyDbContext
    {
        private IMongoClient _mongoClient;
        private IDbSettings _dbSettings;

        public EnergyDbContext(IDbSettings dbSettings)
        {
            _dbSettings = dbSettings;
            _mongoClient = new MongoClient(_dbSettings.MongoConnectionString);
        }

        private IMongoDatabase _mongoDatabase;
        private IMongoDatabase MongoDatabase
        {
            get { return _mongoDatabase ?? (_mongoDatabase = _mongoClient.GetDatabase(_dbSettings.MongoDbName)); }
        }
        
        private IMongoCollection<EnergyReadingMongoModel> _readingCollection;
        public IMongoCollection<EnergyReadingMongoModel> ReadingCollection 
        { 
            get { return _readingCollection ?? (_readingCollection = MongoDatabase.GetCollection<EnergyReadingMongoModel>(_dbSettings.MongoReadingsCollectionName)); } 
        }
        
        private IMongoCollection<MeterMongoModel> _meterCollection;
        public IMongoCollection<MeterMongoModel> MeterCollection 
        { 
            get { return _meterCollection ?? (_meterCollection = MongoDatabase.GetCollection<MeterMongoModel>(_dbSettings.MongoMetersCollectionName)); } 
        }
    }
}