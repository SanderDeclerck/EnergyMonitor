using EnergyMonitor.Service.Model;
using MongoDB.Bson;

namespace EnergyMonitor.Service.Dao
{
    public class MeterMongoModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string EanCode { get; set; }
        public EnergyType Type { get; set; }
        public EnergyUnit Unit { get; set; }
    }
}