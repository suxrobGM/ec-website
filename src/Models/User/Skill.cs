using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.UserModel
{
    public class Skill
    {
        public Skill()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
