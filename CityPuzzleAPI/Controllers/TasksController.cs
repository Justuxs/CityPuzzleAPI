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
    public class TasksController : ControllerBase
    {
        private readonly CityPuzzleContext _context;

        public TasksController(CityPuzzleContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }


        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Model.Task>>> GetTask(int id)
        {
            var obj = _context.Tasks.ToList();
            try
            {
                var selected =await FindListByKey(id, obj);
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

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Model.Task task)
        {
            if (id != task.UserId)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model.Task>> PostTask(Model.Task task)
        {
            _context.Tasks.Add(task);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskExists(task.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTask", new { id = task.UserId }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var obj = _context.Tasks.ToList();
            try
            {
                var task = await FindByKey(id, obj);
                if (task == null)
                {
                    return NotFound();
                }
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch
            {
                return NotFound();
            }
        }

            private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.UserId == id);
        }
        public static async Task<Model.Task> FindByKey(string json, List<Model.Task> tasks)
        {
            List<Model.Task> Tasks = new List<Model.Task>();
            try
            {
                Model.Task obj = JsonConvert.DeserializeObject<Model.Task>(json);
                var rez = tasks.SingleOrDefault(x => (x.PuzzleId == obj.PuzzleId && x.UserId == obj.UserId));
                return rez;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public static async Task<List<Model.Task>> FindListByKey(int json, List<Model.Task> tasks)
        {
            List<Model.Task> Tasks = new List<Model.Task>();
            try
            {
                var values = tasks.Where(x => x.UserId.Equals(json)).ToList();
                return values;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
