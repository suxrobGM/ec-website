using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.ForumModel
{
    public class Thread
    {
        public Thread()
        {
            Id = GeneratorId.GenerateShort();
            Posts = new List<Post>();
            FavoriteThreads = new List<FavoriteThread>();           
        }
     
        public string Id { get; set; }
        public string BoardId { get; set; }
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public bool IsPinned { get; set; }
        public bool IsLocked { get; set; }
        public virtual User Author { get; set; }
        public virtual Board Board { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; }
    }
}
