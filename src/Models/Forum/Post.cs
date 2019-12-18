using System;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.UserModel;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.ForumModel
{
    public class Post
    {
        public Post()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        [StringLength(32)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the post content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        [StringLength(32)]
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        [StringLength(32)]
        public string ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
