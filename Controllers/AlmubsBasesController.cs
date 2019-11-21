using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Test2.Models;

namespace Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmubsBasesController : ControllerBase
    {
        private readonly AlbumsContext _context;

        public AlmubsBasesController(AlbumsContext context)
        {
            _context = context;
        }

        // GET: api/AlmubsBases
        [HttpGet]
        public ActionResult<IEnumerable<AlmubsBase>> GetAlmubsBases()
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://jsonplaceholder.typicode.com/albums");
            List<AlmubsBase> Albums = JsonConvert.DeserializeObject<List<AlmubsBase>>(response);

            return Albums;
        }

        // GET: api/AlmubsBases/ 0 - usr 1 - alb
        [HttpGet("{type}/{id}")]
        public ActionResult<IEnumerable<AlmubsBase>> GetAlmubsBase(int type, int id)
        {
            WebClient client = new WebClient();
            string responseBody;
            if (type == 0)
                responseBody = client.DownloadString("http://jsonplaceholder.typicode.com/albums?userId=" + id + "");
            else
                responseBody = client.DownloadString("http://jsonplaceholder.typicode.com/albums?id=" + id + "");

            List<AlmubsBase> Albums = JsonConvert.DeserializeObject<List<AlmubsBase>>(responseBody);

            return Albums;
        }

        // PUT: api/AlmubsBases/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlmubsBase(int id, AlmubsBase almubsBase)
        {
            return NoContent();
        }

        // POST: api/AlmubsBases
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AlmubsBase>> PostAlmubsBase(AlmubsBase almubsBase)
        {
            return NoContent();
        }

        // DELETE: api/AlmubsBases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AlmubsBase>> DeleteAlmubsBase(int id)
        {
            return NoContent();
        }

        private bool AlmubsBaseExists(int id)
        {
            return _context.AlmubsBases.Any(e => e.Id == id);
        }
    }
}
