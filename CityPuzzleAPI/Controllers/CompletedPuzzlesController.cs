using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPuzzleAPI.Model;
using System.Data.SqlClient;

namespace CityPuzzleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedPuzzlesController : ControllerBase
    {

        // GET: CompletedPuzzles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompletedPuzzle>>> GetCompletedPuzzles()
        {

            string sql = "Select CompletedTaskId,UserId,PuzzleId,Score from CompletedPuzzles";
            List<CompletedPuzzle> CompletedPuzzles = new List<CompletedPuzzle>();
            using (SqlConnection conn = new SqlConnection(CityPuzzleContext.ConnectionString))
            {
                SqlCommand command;
                SqlDataReader dataReader;
                conn.Open();
                command = new SqlCommand(sql, conn);
                dataReader = command.ExecuteReader();
                List<CompletedPuzzle> puzzles = new List<CompletedPuzzle>();
                while (dataReader.Read())
                {
                    CompletedPuzzle temp = new CompletedPuzzle()
                    {
                        CompletedPuzzleId = dataReader.GetInt32(0),
                        UserId = dataReader.GetInt32(1),
                        PuzzleId = dataReader.GetInt32(2),
                        Score = dataReader.GetInt32(3)
                    };
                    CompletedPuzzles.Add(temp);
                }
                conn.Close();
                return CompletedPuzzles;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CompletedPuzzle>>> GetCompletedPuzzles(int id)
        {
            string sql = "Select CompletedTaskId,UserId,PuzzleId,Score from CompletedPuzzles Where UserID=@UserID";
            List<CompletedPuzzle> CompletedPuzzles = new List<CompletedPuzzle>();
            using (SqlConnection conn = new SqlConnection(CityPuzzleContext.ConnectionString))
            {
                SqlCommand command;
                SqlDataReader dataReader;
                conn.Open();
                command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@UserID", id);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    CompletedPuzzle temp = new CompletedPuzzle()
                    {
                        CompletedPuzzleId = dataReader.GetInt32(0),
                        UserId = dataReader.GetInt32(1),
                        PuzzleId = dataReader.GetInt32(2),
                        Score = dataReader.GetInt32(3)
                    };
                    CompletedPuzzles.Add(temp);
                }
                conn.Close();
                return CompletedPuzzles;
            }
        }
        // --------------------------------------------
        [HttpPost]
        public async Task<ActionResult<CompletedPuzzle>> PostCompletedPuzzle(CompletedPuzzle completedPuzzle)
        {
            List<CompletedPuzzle> CompletedPuzzles = new List<CompletedPuzzle>();
            using (SqlConnection conn = new SqlConnection(CityPuzzleContext.ConnectionString))
            {
                conn.Open();
                var command = new SqlCommand("INSERT INTO CompletedPuzzles (UserId,PuzzleId,Score) output inserted.CompletedTaskId VALUES (@UserId,@PuzzleId,@Score)", conn);
                command.Parameters.AddWithValue("@UserId", completedPuzzle.UserId);
                command.Parameters.AddWithValue("@PuzzleId", completedPuzzle.PuzzleId);
                command.Parameters.AddWithValue("@Score", completedPuzzle.Score);
                int id = (int)command.ExecuteScalar();
                completedPuzzle.CompletedPuzzleId = id;
                conn.Close();
                return completedPuzzle;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompletedPuzzle(int id, CompletedPuzzle completedPuzzle)
        {
            try
            {
                List<CompletedPuzzle> CompletedPuzzles = new List<CompletedPuzzle>();
                using (SqlConnection conn = new SqlConnection(CityPuzzleContext.ConnectionString))
                {
                    conn.Open();
                    var command = new SqlCommand("Update CompletedPuzzles set UserId,PuzzleId,Score where CompletedTaskId=@CompletedPuzzleId VALUES (@UserId,@PuzzleId,@Score)", conn);
                    command.Parameters.AddWithValue("@CompletedPuzzleId", id);
                    command.Parameters.AddWithValue("@UserId", completedPuzzle.UserId);
                    command.Parameters.AddWithValue("@PuzzleId", completedPuzzle.PuzzleId);
                    command.Parameters.AddWithValue("@Score", completedPuzzle.Score);
                    command.ExecuteNonQuery();
                    conn.Close();
                    return NoContent();
                }
            }
            catch (SqlException ex)
            {
                return BadRequest();
            }
        }


        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> CompletedPuzzle(int id)
        {
            string sql = "DELETE from CompletedPuzzles where CompletedTaskId=@CompletedPuzzleId";
            using (SqlConnection conn = new SqlConnection(CityPuzzleContext.ConnectionString))
            {
                conn.Open();
                var command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@CompletedPuzzleId", id);
                command.ExecuteNonQuery();
                conn.Close();

            }

            return NoContent();
        }

    }
}
