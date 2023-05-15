using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TrainsAPI.Data;
using TrainsAPI.Models;

namespace TrainsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {
        private readonly TrainsDataContext _context;

        public JourneysController(TrainsDataContext context)
        {
            _context = context;
        }

        // GET: api/Journeys
        [HttpGet]
        public async Task<ActionResult<List<JourneyViewModel>>> GetJourneys()
        {

            var a = await _context.Journeys.Include(x => x.Train)
                .Include(x => x.JourneyPaths).ThenInclude(x => x.Road).ThenInclude(x => x.From)
                .Include(x => x.JourneyPaths).ThenInclude(x => x.Road).ThenInclude(x => x.To)
                .Select(x => x.toJourneyViewModel()).ToListAsync();
            return a;
        }

        // GET: api/Journeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Journey>> GetJourney(int id)
        {
            var journey = await _context.Journeys.FindAsync(id);

            if (journey == null)
            {
                return NotFound();
            }

            return journey;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJourney(int id, Journey journey)
        {
            if (id != journey.Id)
            {
                return BadRequest();
            }

            _context.Entry(journey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JourneyExists(id))
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

        // POST: api/Journeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Journey>> PostJourney(PostJourneyModel model)
        {
            Journey journey = null;
            try
            {
                journey = new Journey
                {
                    Code = model.Code,
                    TrainId = model.TrainId,
                    Departure = DateTime.Parse(model.Departure),
                };
                _context.Journeys.Add(journey);
                await _context.SaveChangesAsync();

               int idJourney = (await _context.Journeys.Select(x=>new {x.Id,x.Code}).FirstAsync(x => x.Code == model.Code)).Id;
                foreach (var id in model.roadsIds)
                {
                    _context.Add(new JourneyRoad { JourneyId = idJourney, RoadId = id });
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CreatedAtAction("GetJourney", new { id = journey == null ? 0 : journey.Id }, journey);

        }

        // DELETE: api/Journeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJourney(int id)
        {
            var journey = await _context.Journeys.FindAsync(id);
            if (journey == null)
            {
                return NotFound();
            }

            _context.Journeys.Remove(journey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JourneyExists(int id)
        {
            return _context.Journeys.Any(e => e.Id == id);
        }
    }
}
