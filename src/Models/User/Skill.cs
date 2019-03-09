using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.UserModel
{
    public class Skill
    {
        public Skill()
        {
            Id = GeneratorId.Generate();
            UserSkills = new List<UserSkill>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
