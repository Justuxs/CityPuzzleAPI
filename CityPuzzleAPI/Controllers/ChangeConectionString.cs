using CityPuzzleAPI.Model;
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
    [Route("[controller]")]
    [LogAspect]


    public class ChangeConectionString : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PostUser(string conn)
        {
            if ((String.IsNullOrWhiteSpace(conn)) || (String.IsNullOrWhiteSpace(conn)))
            {
                return BadRequest();
            }
            Console.WriteLine(conn);
            CityPuzzleContext.ConnectionString = conn;
            return NoContent();
        }
        // GET: ChangeConectionString/Delete/5

        //Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C:\\USERS\\JUSTA\\SOURCE\\REPOS\\APIFORTESTS\\APIFORTESTS\\DATABASE1.MDF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;

    }
}
