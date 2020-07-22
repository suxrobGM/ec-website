using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.BlogModel
{
    public class BlogLike
    {
        [StringLength(32)]
        public string BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [StringLength(32)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
