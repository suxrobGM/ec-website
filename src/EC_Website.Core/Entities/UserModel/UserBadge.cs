namespace EC_Website.Core.Entities.UserModel
{
    public class UserBadge
    {
        public virtual ApplicationUser User { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
