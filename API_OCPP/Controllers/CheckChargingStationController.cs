using API_OCPP.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_OCPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckChargingStationController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CheckChargingStationController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet("{serial}")]
        public async Task<ActionResult<bool>> CheckChargingStation(string serial)
        {
            var chargingStation = _context.ChargingStations.Any(cs => cs.SerialNumber == serial);

            if (chargingStation == false)
            {
                return false;
            }
            return true;
        }

    }
}
