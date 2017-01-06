using EnergyMonitor.Service.Dao;
using EnergyMonitor.Service.Model;

namespace EnergyMonitor.Service.BL
{
    public interface IReadingsLogicController
    {
        void InsertReading(EnergyReading reading);
    }

    public class ReadingsLogicController : IReadingsLogicController
    {
        private readonly IReadingsDao _readingsDao;

        public ReadingsLogicController(IReadingsDao readingsDao)
        {
            _readingsDao = readingsDao;
        }

        public void InsertReading(EnergyReading reading)
        {
            _readingsDao.InsertReading(reading);
        }
    }
}