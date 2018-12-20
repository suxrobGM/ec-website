using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class Post
    {
        public Post()
        {
            Id = GeneratorId.Generate("post");
        }
     
        public string Id { get; set; }
        public string ThreadId { get; set; }
        public string AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime? CreatedTime { get; set; }
        public virtual User Author { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
