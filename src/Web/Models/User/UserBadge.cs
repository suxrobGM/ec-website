using System.ComponentModel.DataAnnotations;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.UserModel
{
    public class UserBadge
    {
        [StringLength(32)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(32)]
        public string BadgeId { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
