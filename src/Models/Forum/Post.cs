using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.ForumModel
{
    public class Post
    {
        public Post()
        {
            Id = GeneratorId.GenerateLong();
            CreatedTime = DateTime.Now;
        }
     
        public string Id { get; set; }              
        public bool IsPinned { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public DateTime? CreatedTime { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
