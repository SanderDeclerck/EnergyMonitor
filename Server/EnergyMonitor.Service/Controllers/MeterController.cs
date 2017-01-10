using EnergyMonitor.Service.BL;
using EnergyMonitor.Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitor.Service.Controllers
{
    [Route("api/[controller]")]
    public class MeterController : Controller
    {
        private IMeterLogicController _meterLogicController;
        public MeterController(IMeterLogicController meterLogicController)
        {
            _meterLogicController = meterLogicController;
        }

        // POST api/meter
        [HttpPost]
        public void Post([FromBody]Meter meter)
        {
            _meterLogicController.InsertMeter(meter);
        }
    }
}