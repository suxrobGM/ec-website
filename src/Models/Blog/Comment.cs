using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;
using EC_WebSite.Models.UserModel;

namespace EC_WebSite.Models.Blog
{
    public class Comment
    {
        public Comment()
        {
            Id = GeneratorId.GenerateLong();
            CreatedDate = DateTime.Now;
            Replies = new List<Comment>();
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string BlogId { get; set; }
        public virtual Article Blog { get; set; }
        
        public string ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}