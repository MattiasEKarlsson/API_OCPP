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
    public class HeartbeatsController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public HeartbeatsController(SqlDbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heartbeat>>> GetHeartbeats()
        {
            return await _context.Heartbeats.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Heartbeat>> GetHeartbeat(int id)
        {
            var heartbeat = await _context.Heartbeats.FindAsync(id);

            if (heartbeat == null)
            {
                return NotFound();
            }

            return heartbeat;
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeartbeat(int id, Heartbeat heartbeat)
        {
            if (id != heartbeat.Id)
            {
                return BadRequest();
            }

            _context.Entry(heartbeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeartbeatExists(id))
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

      
        [HttpPost("{ip}")]
        public async Task<ActionResult> PostHeartbeat(string ip)
        {
            Heartbeat hb = new Heartbeat();

            var user = await _context.ChargingStations.FirstOrDefaultAsync(u => u.Ip == ip);


            hb.ChargingStationsId = user.Id;
            hb.Hbtime = DateTime.Now.AddHours(2);
            _context.Heartbeats.Add(hb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeartbeat(int id)
        {
            var heartbeat = await _context.Heartbeats.FindAsync(id);
            if (heartbeat == null)
            {
                return NotFound();
            }

            _context.Heartbeats.Remove(heartbeat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllHeartbeat()
        {
            var all = from c in _context.Heartbeats select c;

            _context.Heartbeats.RemoveRange(all);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeartbeatExists(int id)
        {
            return _context.Heartbeats.Any(e => e.Id == id);
        }
    }
}
