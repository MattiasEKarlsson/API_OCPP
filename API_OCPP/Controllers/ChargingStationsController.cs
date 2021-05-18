using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OCPP.Data;
using API_OCPP.Models;

namespace API_OCPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargingStationsController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public ChargingStationsController(SqlDbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChargingStation>>> GetChargingStations()
        {
            return await _context.ChargingStations.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<ChargingStation>> GetChargingStation(int id)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(id);

            if (chargingStation == null)
            {
                return NotFound();
            }

            return chargingStation;
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChargingStation(int id, ChargingStation chargingStation)
        {
            if (id != chargingStation.Id)
            {
                return BadRequest();
            }

            _context.Entry(chargingStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChargingStationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<ChargingStation>> PostChargingStation(ChargingStation chargingStation)
        {
            _context.ChargingStations.Add(chargingStation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChargingStation", new { id = chargingStation.Id }, chargingStation);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChargingStation(int id)
        {
            var chargingStation = await _context.ChargingStations.FindAsync(id);
            if (chargingStation == null)
            {
                return NotFound();
            }

            _context.ChargingStations.Remove(chargingStation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChargingStationExists(int id)
        {
            return _context.ChargingStations.Any(e => e.Id == id);
        }
    }
}
