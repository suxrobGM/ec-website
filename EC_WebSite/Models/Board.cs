using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class Board
    {
        public Board()
        {
            Threads = new List<Thread>();
            Id = GeneratorId.Generate("board");
        }

        public string Id { get; set; }
        public string ForumId { get; set; }
        public string Name { get; set; }
        public virtual ForumHeader Forum { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
    }
}
