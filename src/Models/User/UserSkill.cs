
// ReSharper disable once CheckNamespace
namespace EC_Website.Models.UserModel
{
    public class UserSkill
    {               
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
