using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.BlogModel
{
    public class Comment : EntityBase
    {
        [Required(ErrorMessage = "Please enter content")]
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}