using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.UserModel;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.ForumModel
{
    public class Thread
    {
        public Thread()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }
     
        public string Id { get; set; }             
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool IsPinned { get; set; }
        public bool IsLocked { get; set; }
        public DateTime Timestamp { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; } = new List<FavoriteThread>();
    }
}
