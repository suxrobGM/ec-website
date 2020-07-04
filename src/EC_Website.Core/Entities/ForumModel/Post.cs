using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.ForumModel
{
    public class Post : EntityBase
    {
        [Required(ErrorMessage = "Please enter the post content")]
        public string Content { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
