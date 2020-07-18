using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.ForumModel
{
    public class Post : EntityBase
    {
        [Required(ErrorMessage = "Please enter the post content")]
        [Display(Description = "Content")]
        public string Content { get; set; }

        [Display(Name = "Author")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "Thread")]
        public virtual Thread Thread { get; set; }
    }
}
