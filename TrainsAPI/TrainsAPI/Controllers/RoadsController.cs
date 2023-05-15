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
    public class RoadsController : ControllerBase
    {
        private readonly TrainsDataContext _context;

        public RoadsController(TrainsDataContext context)
        {
            _context = context;
        }

        // GET: api/Roads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Road>>> GetRoads()
        {
            var a = await _context.Roads.Include(x => x.From).Include(x=>x.To).Include(x => x.To).ToListAsync();
            return a;
        }

        // GET: api/Roads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Road>> GetRoad(int id)
        {
            var road = await _context.Roads.FindAsync(id);

            if (road == null)
            {
                return NotFound();
            }

            return road;
        }

        // PUT: api/Roads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoad(int id, Road road)
        {
            if (id != road.Id)
            {
                return BadRequest();
            }

            _context.Entry(road).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoadExists(id))
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

        // POST: api/Roads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Road>> PostRoad(Road road)
        {
            _context.Roads.Add(road);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoad", new { id = road.Id }, road);
        }

        // DELETE: api/Roads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoad(int id)
        {
            var road = await _context.Roads.FindAsync(id);
            if (road == null)
            {
                return NotFound();
            }

            _context.Roads.Remove(road);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoadExists(int id)
        {
            return _context.Roads.Any(e => e.Id == id);
        }
    }
}
