using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.User;

namespace EC_Website.Core.Entities.Forum
{
    public class Post : EntityBase
    {
        [Required(ErrorMessage = "Please enter the post content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [StringLength(32)]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        [StringLength(32)]
        public string ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
