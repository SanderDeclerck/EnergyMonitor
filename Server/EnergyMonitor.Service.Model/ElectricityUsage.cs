namespace EnergyMonitor.Service.Model
{
    public class ElectricityUsage : EnergyUsage
    {
        public override EnergyType Type { get { return EnergyType.Electricity; } }
        public override EnergyUnit Unit { get { return EnergyUnit.Kwh; } }

        public override double Value { get; set; }
    }
}