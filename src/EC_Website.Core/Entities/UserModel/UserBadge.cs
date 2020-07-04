using System.ComponentModel.DataAnnotations;

namespace EC_Website.Core.Entities.UserModel
{
    public class UserBadge
    {
        [StringLength(32)]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [StringLength(32)]
        public string BadgeId { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
