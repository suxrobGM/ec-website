using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class FavoriteThread
    {
        public string ThreadId { get; set; }
        public string UserId { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual User User { get; set; }
    }
}
