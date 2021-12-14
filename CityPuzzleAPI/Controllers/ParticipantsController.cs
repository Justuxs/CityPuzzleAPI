using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityPuzzleAPI.Model;
using Newtonsoft.Json;
using CityPuzzleAPI.Aspects;

namespace CityPuzzleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LogAspect]
    public class ParticipantsController : ControllerBase
    {
        private readonly CityPuzzleContext _context;

        public ParticipantsController(CityPuzzleContext context)
        {
            _context = context;
        }

        // GET: api/Participants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
        {
            return await _context.Participants.ToListAsync();
        }

        // GET: api/Participants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipant(int id)
        {
            var obj = _context.Participants.ToList();
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

        // PUT: api/Participants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipant(int id, Participant participant)
        {
            if (id != participant.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(participant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/Participants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            _context.Participants.Add(participant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticipantExists(participant.RoomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParticipant", new { id = participant.RoomId }, participant);
        }

        // DELETE: api/Participants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(string id)
        {
            var obj = _context.Participants.ToList();
            try
            {
                var participant = await FindByKey(id, obj);
                if (participant == null)
                {
                    return NotFound();
                }
                _context.Participants.Remove(participant);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch
            {
                return NotFound();
            }
        }
        public static async Task<Participant> FindByKey(string json, List<Participant> participants)
        {
            try
            {
                Participant obj = JsonConvert.DeserializeObject<Participant>(json);
                var rez = participants.SingleOrDefault(x => (x.RoomId == obj.RoomId && x.UserId == obj.UserId));
                return rez;
            }
            catch(Exception e)
            {
                throw;
            }
        }
        private bool ParticipantExists(int id)
        {
            return _context.Participants.Any(e => e.RoomId == id);
        }
        public static async Task<List<Participant>> FindListByKey(int roomid, List<Participant> participants)
        {
            List<Participant> Tasks = new List<Participant>();
            try
            {
                var values = participants.Where(x => x.RoomId.Equals(roomid)).ToList();
                return values;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
