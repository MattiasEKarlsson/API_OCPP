using API_OCPP.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_OCPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckUserController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public CheckUserController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<bool>> CheckUsers(string key)
        {
            var chargingStation = await _context.Users.FirstOrDefaultAsync(x=> x.TagId == key);

            if (chargingStation == null)
            {
                return false;
            }
            return true;
        }


    }
}
