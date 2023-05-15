using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainsAPI.Data;
using TrainsAPI.Models;

namespace TrainsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly TrainsDataContext _context;

        public TrainsController(TrainsDataContext context)
        {
            _context = context;
        }

        // GET: api/Trains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainViewModel>>> GetTrains()
        {
            return await _context.Trains.Include(x => x.Engine).Include(x => x.Cars).Select(x=>x.toTrainViewModel()).ToListAsync();
        }

        // GET: api/Trains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrain(int id)
        {
            var train = await _context.Trains.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            return train;
        }

        // PUT: api/Trains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrain(PostTrainModel model)
        {
            var train = await _context.Trains.FirstOrDefaultAsync(x => x.Id == model.Train.Id);
            train.EngineId = model.Train.EngineId;
            train.Code = model.Train.Code;

            foreach(var car in (await _context.Cars.Where(x=>x.TrainId== model.Train.Id).ToListAsync()))
            {
                car.TrainId = null;
            }

            foreach (var carId in model.CarIds)
            {
                (await _context.Cars.FindAsync(carId)).TrainId = train.Id;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrain", new { id = model.Train.Id }, model.Train);
        }

        // POST: api/Trains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Train>> PostTrain(PostTrainModel model)
        {
            _context.Trains.Add(model.Train);
            await _context.SaveChangesAsync();
            var train = await _context.Trains.FirstOrDefaultAsync(x=>x.Code == model.Train.Code);
            foreach (var item in model.CarIds)
            {
               (await _context.Cars.FindAsync(item)).TrainId = train.Id;
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrain", new { id = model.Train.Id }, model.Train);
        }

        // DELETE: api/Trains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainExists(int id)
        {
            return _context.Trains.Any(e => e.Id == id);
        }
    }
}
