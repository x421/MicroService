using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test2.Models
{
    public class UsersBase
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Username { get; set; }

        public String Email { get; set; }

        public class Address
        {
            public String Street { get; set; }

            public String Suite { get; set; }

            public String City { get; set; }

            public String Zipcode { get; set; }

            public class Geo
            {
                public double Lat { get; set; }

                public double Lng { get; set; }
            }

            public Geo geo { get; set; }
        }

        public Address address { get; set; }

        public String Phone { get; set; }

        public String Website { get; set; }

        public class Company
        {
            public String Name { get; set; }

            public String CatchPhrase { get; set; }

            public String Bs { get; set; }
        }

        public Company company { get; set; }
    }
}
