using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.BlogModel
{
    public class Comment : EntityBase
    {
        [Required(ErrorMessage = "Please enter content")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Author")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "Blog")]
        public virtual Blog Blog { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}