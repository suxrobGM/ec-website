using System.ComponentModel.DataAnnotations;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.UserModel
{
    public class UserSkill
    {
        [StringLength(20)]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [StringLength(20)]
        public string SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
