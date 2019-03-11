using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.Blog
{
    public class CommentReply
    {
        public CommentReply()
        {
            Id = GeneratorId.Generate();
            CreatedDate = DateTime.Now;
        }

        public string Id { get; set; }
        public string Text { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public string CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
