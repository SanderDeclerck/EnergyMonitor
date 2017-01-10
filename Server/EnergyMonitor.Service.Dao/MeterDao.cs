using System;
using EnergyMonitor.Service.Model;
using MongoDB.Bson;

namespace EnergyMonitor.Service.Dao
{
    public interface IMeterDao
    {
        void InsertMeter(Meter meter);
    }

    public class MeterDao : IMeterDao
    {
        public IEnergyDbContext _energyDbContext;

        public MeterDao(IEnergyDbContext energyDbContext)
        {
            _energyDbContext = energyDbContext;
        }

        public void InsertMeter(Meter meter)
        {
            var meterModel = new MeterMongoModel
            {
                Id = ObjectId.GenerateNewId(),
                Name = meter.Name,
                EanCode = meter.EanCode,
                Type = meter.Type,
                Unit = meter.Unit
            };
            _energyDbContext.MeterCollection.InsertOne(meterModel);
            meter.Id = meterModel.Id.ToString();
        }
    }
}