using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.UserModel;

namespace EC_Website.Models.ForumModel
{
    public class Thread
    {
        public Thread()
        {
            Id = GeneratorId.GenerateLong();
            Posts = new List<Post>();
            FavoriteThreads = new List<FavoriteThread>();
            Timestamp = DateTime.Now;
        }
     
        public string Id { get; set; }             
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsPinned { get; set; }
        public bool IsLocked { get; set; }
        public DateTime Timestamp { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; }

        public void GenerateUrl()
        {
            Url = $"{Name.Trim().Replace(' ', '_')}";
        }
    }
}
