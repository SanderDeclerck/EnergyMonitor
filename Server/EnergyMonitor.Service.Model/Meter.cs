namespace EnergyMonitor.Service.Model
{
    public class Meter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EanCode { get; set; }
        public EnergyType Type { get; set; }
        public EnergyUnit Unit { get; set; }
    }
}