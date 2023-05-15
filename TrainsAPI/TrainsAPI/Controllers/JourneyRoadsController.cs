using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainsAPI.Data;
using TrainsAPI.Models;

namespace TrainsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyRoadsController : ControllerBase
    {
        private readonly TrainsDataContext _context;

        public JourneyRoadsController(TrainsDataContext context)
        {
            _context = context;
        }

        // GET: api/JourneyRoads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JourneyRoad>>> GetJourneyRoads()
        {
            return await _context.JourneyRoads.ToListAsync();
        }

        // GET: api/JourneyRoads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JourneyRoad>> GetJourneyRoad(int id)
        {
            var journeyRoad = await _context.JourneyRoads.FindAsync(id);

            if (journeyRoad == null)
            {
                return NotFound();
            }

            return journeyRoad;
        }

        // PUT: api/JourneyRoads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJourneyRoad(int id, JourneyRoad journeyRoad)
        {
            if (id != journeyRoad.Id)
            {
                return BadRequest();
            }

            _context.Entry(journeyRoad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourneyRoadExists(id))
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

        // POST: api/JourneyRoads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JourneyRoad>> PostJourneyRoad(JourneyRoad journeyRoad)
        {
            _context.JourneyRoads.Add(journeyRoad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJourneyRoad", new { id = journeyRoad.Id }, journeyRoad);
        }

        // DELETE: api/JourneyRoads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJourneyRoad(int id)
        {
            var journeyRoad = await _context.JourneyRoads.FindAsync(id);
            if (journeyRoad == null)
            {
                return NotFound();
            }

            _context.JourneyRoads.Remove(journeyRoad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JourneyRoadExists(int id)
        {
            return _context.JourneyRoads.Any(e => e.Id == id);
        }
    }
}
