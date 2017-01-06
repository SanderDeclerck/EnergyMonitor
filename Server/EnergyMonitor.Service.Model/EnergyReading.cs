using System;

namespace EnergyMonitor.Service.Model
{
    public class EnergyReading
    {
        public DateTime Timestamp { get; set; }
        public EnergyUnit Unit { get; set; }
        public EnergyType Type { get; set; }
        public double Value { get; set; }
    }
}