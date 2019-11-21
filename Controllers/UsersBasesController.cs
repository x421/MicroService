using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Test2.Models;

namespace Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersBasesController : ControllerBase
    {
        private readonly UsersContext _context;

        public UsersBasesController(UsersContext context)
        {
            _context = context;
        }

        // GET: api/UsersBases
        [HttpGet]
        public ActionResult<IEnumerable<UsersBase>> GetUsersItems()
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://jsonplaceholder.typicode.com/users");
            List<UsersBase> Users = JsonConvert.DeserializeObject<List<UsersBase>>(response);

            return Users;
        }

        // GET: api/UsersBases/5
        [HttpGet("{id}")]
        public ActionResult<UsersBase> GetUsersBase(int id)
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://jsonplaceholder.typicode.com/users/"+id+"");

            UsersBase Users = JsonConvert.DeserializeObject<UsersBase>(response);

            return Users;
        }

        // PUT: api/UsersBases/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersBase(int id, UsersBase usersBase)
        {
            return NoContent();
        }

        // POST: api/UsersBases
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UsersBase>> PostUsersBase(UsersBase usersBase)
        {
            return NoContent();
        }

        // DELETE: api/UsersBases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UsersBase>> DeleteUsersBase(int id)
        {
            return NoContent();
        }

        private bool UsersBaseExists(int id)
        {
            return _context.UsersItems.Any(e => e.Id == id);
        }
    }
}
