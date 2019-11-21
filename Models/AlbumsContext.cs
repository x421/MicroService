using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class AlbumsContext : DbContext
    {
        public AlbumsContext(DbContextOptions<AlbumsContext> options)
            : base(options)
        {
        }

        public DbSet<AlmubsBase> AlmubsBases { get; set; }
    }


}
