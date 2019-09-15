using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;

namespace EC_WebSite.Models.UserModel
{
    public class Skill
    {
        public Skill()
        {
            Id = GeneratorId.GenerateLong();
            UserSkills = new List<UserSkill>();
            Timestamp = DateTime.Now;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
