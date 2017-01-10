using System;
using MongoDB.Bson;

namespace EnergyMonitor.Service.Dao
{
    public class EnergyReadingMongoModel
    {
        public ObjectId Id { get; set; }
        public ObjectId MeterId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}