using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.ForumModel
{
    public class Board
    {
        public Board()
        {
            Id = GeneratorId.GenerateLong();
            Threads = new List<Thread>();            
        }

        public string Id { get; set; }       
        public string Name { get; set; }

        public string ForumId { get; set; }
        public virtual ForumHead Forum { get; set; }

        public virtual ICollection<Thread> Threads { get; set; }
    }
}
