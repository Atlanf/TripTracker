using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTracker.BackService.Data;
using TripTracker.BackService.Model;

namespace TripTracker.BackService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private TripContext _context;

        public TripsController(TripContext context)
        {
            _context = context;
        }

        // GET api/trips
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trips = await _context.Trips.ToListAsync();
            return Ok(trips);
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(int id)
        {
            return _context.Trips.Find(id);
        }

        // POST api/trips
        [HttpPost]
        public IActionResult Post([FromBody] Trip value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Add(value);
            _context.SaveChanges();

            return Ok();
        }

        // PUT api/trips/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Trip value)
        {
            if (!_context.Trips.Any(t => t.Id == id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Update(value);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/trips/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var myTrip = _context.Trips.Find(id);

            if (myTrip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(myTrip);
            _context.SaveChanges();

            return NoContent();
        }
    }
}