using System;
using EnergyMonitor.Service.Dao;
using EnergyMonitor.Service.Model;

namespace EnergyMonitor.Service.BL
{
    public interface IMeterLogicController 
    {
        void InsertMeter(Meter meter);
    }

    public class MeterLogicController : IMeterLogicController
    {
        private IMeterDao _meterDao;
        public MeterLogicController(IMeterDao meterDao)
        {
            _meterDao = meterDao;
        }

        public void InsertMeter(Meter meter)
        {
            _meterDao.InsertMeter(meter);
        }
    }
}