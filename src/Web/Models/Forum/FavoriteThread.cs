using System.ComponentModel.DataAnnotations;
using EC_Website.Models.UserModel;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.ForumModel
{
    public class FavoriteThread
    {
        [StringLength(32)]
        public string ThreadId { get; set; }        
        public virtual Thread Thread { get; set; }

        [StringLength(32)]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
