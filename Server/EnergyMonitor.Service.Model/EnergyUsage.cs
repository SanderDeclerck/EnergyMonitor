using System;

namespace EnergyMonitor.Service.Model
{
    public abstract class EnergyUsage
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public abstract double Value { get; set; }
        public abstract EnergyType Type { get; }
        public abstract EnergyUnit Unit { get; }
    }
}