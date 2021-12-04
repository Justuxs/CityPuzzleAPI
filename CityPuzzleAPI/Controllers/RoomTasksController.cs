using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityPuzzleAPI.Model;
using Newtonsoft.Json;

namespace CityPuzzleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTasksController : ControllerBase
    {
        private readonly CityPuzzleContext _context;

        public RoomTasksController(CityPuzzleContext context)
        {
            _context = context;
        }

        // GET: api/RoomTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTask>>> GetRoomTasks()
        {
            return await _context.RoomTasks.ToListAsync();
        }

        // GET: api/RoomTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RoomTask>>> GetRoomTask(int id)
        {
            var obj = _context.RoomTasks.ToList();
            try
            {
                var selected = await FindListByKey(id, obj);
                if (selected.Count == 0)
                {
                    return NotFound();
                }
                else return selected;
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/RoomTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomTask(int id, RoomTask roomTask)
        {
            if (id != roomTask.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(roomTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomTaskExists(id))
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

        // POST: api/RoomTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomTask>> PostRoomTask(RoomTask roomTask)
        {
            _context.RoomTasks.Add(roomTask);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoomTaskExists(roomTask.RoomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRoomTask", new { id = roomTask.RoomId }, roomTask);
        }

        // DELETE: api/RoomTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomTask(string id)
        {
            var obj = _context.RoomTasks.ToList();
            try
            {
                var RoomTask = await FindByKey(id, obj);
                if (RoomTask == null)
                {
                    return NotFound();
                }
                _context.RoomTasks.Remove(RoomTask);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch
            {
                return NotFound();
            }
        }
        public static async Task<RoomTask> FindByKey(string json, List<RoomTask> participants)
        {
            try
            {
                RoomTask obj = JsonConvert.DeserializeObject<RoomTask>(json);
                var rez = participants.SingleOrDefault(x => (x.RoomId == obj.RoomId && x.PuzzleId == obj.PuzzleId));
                return rez;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private bool RoomTaskExists(int id)
        {
            return _context.RoomTasks.Any(e => e.RoomId == id);
        }
        public static async Task<List<RoomTask>> FindListByKey(int json, List<RoomTask> tasks)
        {
            List<RoomTask> Tasks = new List<RoomTask>();
            try
            {
                var values = tasks.Where(x => x.RoomId.Equals(json)).ToList();
                return values;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
