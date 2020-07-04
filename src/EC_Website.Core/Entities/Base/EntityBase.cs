using System;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Interfaces;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Core.Entities.Base
{
    public abstract class EntityBase : IEntity<string>
    {
        protected EntityBase()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        [StringLength(32)]
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
