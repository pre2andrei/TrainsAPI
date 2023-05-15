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
    public class EngineTypesController : ControllerBase
    {
        private readonly TrainsDataContext _context;

        public EngineTypesController(TrainsDataContext context)
        {
            _context = context;
        }

        // GET: api/EngineTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EngineType>>> GetEngineTypes()
        {
            return await _context.EngineTypes.ToListAsync();
        }

        // GET: api/EngineTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EngineType>> GetEngineType(int id)
        {
            var engineType = await _context.EngineTypes.FindAsync(id);

            if (engineType == null)
            {
                return NotFound();
            }

            return engineType;
        }

        // PUT: api/EngineTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEngineType(int id, EngineType engineType)
        {
            if (id != engineType.Id)
            {
                return BadRequest();
            }

            _context.Entry(engineType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineTypeExists(id))
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

        // POST: api/EngineTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EngineType>> PostEngineType(EngineType engineType)
        {
            _context.EngineTypes.Add(engineType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEngineType", new { id = engineType.Id }, engineType);
        }

        // DELETE: api/EngineTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEngineType(int id)
        {
            var engineType = await _context.EngineTypes.FindAsync(id);
            if (engineType == null)
            {
                return NotFound();
            }

            _context.EngineTypes.Remove(engineType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EngineTypeExists(int id)
        {
            return _context.EngineTypes.Any(e => e.Id == id);
        }
    }
}
