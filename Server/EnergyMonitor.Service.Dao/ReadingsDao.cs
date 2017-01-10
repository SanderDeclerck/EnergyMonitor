using EnergyMonitor.Service.Model;
using MongoDB.Bson;

namespace EnergyMonitor.Service.Dao
{
    public interface IReadingsDao
    {
        void InsertReading(EnergyReading reading);
    }

    public class ReadingsDao : IReadingsDao
    {
        private IEnergyDbContext _energyDbContext;

        public ReadingsDao(IEnergyDbContext energyDbContext)
        {
            _energyDbContext = energyDbContext;
        }

        public void InsertReading(EnergyReading reading)
        {
            var readingModel = new EnergyReadingMongoModel
            {
                Id = ObjectId.GenerateNewId(),
                MeterId = ObjectId.Parse(reading.MeterId),
                Timestamp = reading.Timestamp,
                Value = reading.Value
            };
            _energyDbContext.ReadingCollection.InsertOne(readingModel);
            reading.Id = readingModel.Id.ToString();
        }
    }
}