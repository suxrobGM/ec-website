using System;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.UserModel;

namespace EC_Website.Models.ForumModel
{
    public class Post
    {
        public Post()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }
     
        public string Id { get; set; }              
        public bool IsPinned { get; set; }

        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
