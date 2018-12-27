﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_WebSite.Models
{
    public class Skill
    {
        public Skill()
        {
            Id = GeneratorId.Generate("skill");
        }

        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public virtual User Owner { get; set; }
    }
}