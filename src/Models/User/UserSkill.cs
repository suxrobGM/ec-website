using System;

namespace EC_WebSite.Models.UserModel
{
    public class UserSkill
    {               
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
