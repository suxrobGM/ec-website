using EC_Website.Models.UserModel;

namespace EC_Website.Models.ForumModel
{
    public class FavoriteThread
    {
        public string ThreadId { get; set; }        
        public virtual Thread Thread { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
