using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.UserModel;

namespace EC_Website.Models.Blog
{
    public class Comment
    {
        public Comment()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
            Replies = new List<Comment>();
        }

        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }
        public string ArticleId { get; set; }
        public virtual BlogArticle Article { get; set; }
        public string ParentId { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Replies { get; set; }       
    }
}