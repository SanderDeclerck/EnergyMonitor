using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EnergyMonitor.Service.Model;
using System;
using EnergyMonitor.Service.BL;

namespace EnergyMonitor.Service.Controllers
{
    [Route("api/[controller]")]
    public class MonthlyController : Controller
    {

        // GET api/monthly
        [HttpGet]
        public IEnumerable<MonthlyReport> Get()
        {
            return new List<MonthlyReport> 
            {
                new MonthlyReport 
                { 
                    UsageData = new List<EnergyUsage> 
                    {
                        new ElectricityUsage 
                        {
                            Start = new DateTime(2016, 1, 2),
                            End = new DateTime(2016, 1, 31),
                            Value = 300.12
                        }
                    } 
                }
            };
        }
    }
}