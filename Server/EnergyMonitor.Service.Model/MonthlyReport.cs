using System.Collections.Generic;

namespace EnergyMonitor.Service.Model
{
    public class MonthlyReport
    {
        public IEnumerable<EnergyUsage> UsageData { get; set; }
    }
}