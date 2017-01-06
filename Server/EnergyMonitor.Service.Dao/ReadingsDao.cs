using EnergyMonitor.Service.Model;

namespace EnergyMonitor.Service.Dao
{
    public interface IReadingsDao
    {
        void InsertReading(EnergyReading reading);
    }

    public class ReadingsDao : IReadingsDao
    {
        public void InsertReading(EnergyReading reading)
        {
            System.Console.WriteLine("Hello from the {0}", this.GetType());
        }
    }
}