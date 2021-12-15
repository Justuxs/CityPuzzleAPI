using CityPuzzleAPI.Model;
using CityPuzzleAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityPuzzleAPI.Aspects;

namespace CityPuzzleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [LogAspect]


    public class ChangeConectionString : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<ConnString>> PostConn(ConnString conn)
        {
            if ((String.IsNullOrWhiteSpace(conn.Conn)) || (String.IsNullOrWhiteSpace(conn.Token) || !conn.Token.Equals("CityPuzzle")))
            {
                return BadRequest();
            }
            Console.WriteLine(conn.Conn);
            CityPuzzleContext.ConnectionString = conn.Conn;
            return CreatedAtAction("PostConn", conn);
        }
        // GET: ChangeConectionString/Delete/5

        //Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\JUSTA\\SOURCE\\REPOS\\APIFORTESTS\\APIFORTESTS\\DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;

    }
}
