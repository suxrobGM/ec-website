using System;
using System.Collections.Generic;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.Blog
{
    public class Comment
    {
        public Comment()
        {
            Id = GeneratorId.GenerateShort();
            CreatedDate = DateTime.Now;
            Replies = new List<CommentReply>();
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string BlogId { get; set; }
        public virtual Article Blog { get; set; }
        
        public virtual ICollection<CommentReply> Replies { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}