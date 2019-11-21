using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
        private AesManaged myAes;
        public UsersBasesController(UsersContext context)
        {
            myAes = new AesManaged();
            _context = context;
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;

        }

        // GET: api/UsersBases
        [HttpGet]
        public ActionResult<IEnumerable<UsersBase>> GetUsersItems()
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://jsonplaceholder.typicode.com/users");
            List<UsersBase> Users = JsonConvert.DeserializeObject<List<UsersBase>>(response);

            for (int i = 0; i < Users.Count; i++)
            {
                byte[] buffer = EncryptStringToBytes_Aes(Users[i].Email, myAes.Key, myAes.IV);
                Users[i].Email = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }

            return Users;
        }

        // GET: api/UsersBases/5
        [HttpGet("{id}")]
        public ActionResult<UsersBase> GetUsersBase(int id)
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://jsonplaceholder.typicode.com/users/"+id+"");

            UsersBase Users = JsonConvert.DeserializeObject<UsersBase>(response);

            byte[] buffer = EncryptStringToBytes_Aes(Users.Email, myAes.Key, myAes.IV);
            Users.Email = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

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
