using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.ForumModel
{
    public class FavoriteThread
    {
        [StringLength(32)]
        public string ThreadId { get; set; }
        public virtual Thread Thread { get; set; }

        [StringLength(32)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
