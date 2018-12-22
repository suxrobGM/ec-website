using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Posts = new List<Post>();
            Threads = new List<Thread>();
        }

        public byte[] ProfilePhoto { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? BanPeriod { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
    }
}
