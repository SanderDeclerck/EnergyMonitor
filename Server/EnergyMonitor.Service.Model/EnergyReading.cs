using System;

namespace EnergyMonitor.Service.Model
{
    public class EnergyReading
    {
        public string Id { get; set; }
        public string MeterId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}