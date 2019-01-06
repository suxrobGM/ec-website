using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models.UserModel
{
    public class Skill
    {
        public Skill()
        {
            Id = GeneratorId.Generate("skill");
            UserSkills = new List<UserSkill>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
