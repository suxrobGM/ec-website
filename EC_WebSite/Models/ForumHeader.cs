using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class ForumHeader
    {
        public ForumHeader()
        {
            Boards = new List<Board>();
            Id = GeneratorId.Generate("forum");
        }
       
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Board> Boards { get; set; }
    }
}
