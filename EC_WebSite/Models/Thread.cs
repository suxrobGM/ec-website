using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class Thread
    {
        public Thread()
        {
            Posts = new List<Post>();
            Id = GeneratorId.Generate("thread");
        }
     
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public virtual User Author { get; set; }
        public virtual Board Board { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
