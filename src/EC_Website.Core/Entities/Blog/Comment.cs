using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;
using EC_Website.Core.Entities.User;

namespace EC_Website.Core.Entities.Blog
{
    public class Comment
    {
        public Comment()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        [StringLength(32)]
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        [StringLength(32)]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        [StringLength(32)]
        public string BlogEntryId { get; set; }
        public virtual BlogEntry Entry { get; set; }

        [StringLength(32)]
        public string ParentId { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}