using EnergyMonitor.Service.BL;
using EnergyMonitor.Service.Model;
using Microsoft.AspNetCore.Mvc;

namespace EnergyMonitor.Service.Controllers
{
    [Route("api/[controller]")]
    public class ReadingsController : Controller
    {
        private readonly IReadingsLogicController _readingsLogicController;
        public ReadingsController(IReadingsLogicController readingsLogicController)
        {
            _readingsLogicController = readingsLogicController;
        }

        // POST api/readings
        [HttpPost]
        public void Post([FromBody]EnergyReading reading)
        {
            _readingsLogicController.InsertReading(reading);
        }
    }
}