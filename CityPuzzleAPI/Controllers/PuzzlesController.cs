using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityPuzzleAPI.Model;
using CityPuzzleAPI.Aspects;

namespace CityPuzzleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogAspect]
    public class PuzzlesController : ControllerBase
    {
        private readonly CityPuzzleContext _context;

        public PuzzlesController(CityPuzzleContext context)
        {
            _context = context;
        }

        // GET: api/Puzzles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puzzle>>> GetPuzzles()
        {
            return await _context.Puzzles.ToListAsync();
        }

        // GET: api/Puzzles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Puzzle>> GetPuzzle(int id)
        {
            var puzzle = await _context.Puzzles.FindAsync(id);

            if (puzzle == null)
            {
                return NotFound();
            }

            return puzzle;
        }

        // PUT: api/Puzzles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuzzle(int id, Puzzle puzzle)
        {
            if (id != puzzle.Id)
            {
                return BadRequest();
            }

            _context.Entry(puzzle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuzzleExists(id))
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

        // POST: api/Puzzles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Puzzle>> PostPuzzle(Puzzle puzzle)
        {
            _context.Puzzles.Add(puzzle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPuzzle", new { id = puzzle.Id }, puzzle);
        }

        // DELETE: api/Puzzles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuzzle(int id)
        {
            var puzzle = await _context.Puzzles.FindAsync(id);
            if (puzzle == null)
            {
                return NotFound();
            }

            _context.Puzzles.Remove(puzzle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuzzleExists(int id)
        {
            return _context.Puzzles.Any(e => e.Id == id);
        }
    }
}
