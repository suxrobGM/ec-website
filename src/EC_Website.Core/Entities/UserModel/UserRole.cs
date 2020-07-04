using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;
using EC_Website.Core.Interfaces;

namespace EC_Website.Core.Entities.UserModel
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Moderator,
        Developer,
        Editor
    }

    public class UserRole : IdentityRole, IEntity<string>
    {
        public UserRole() : this(Role.Editor)
        {
        }

        public UserRole(Role role): base(role.ToString())
        {
            Id = GeneratorId.GenerateLong();
            Role = role;
            Timestamp = DateTime.Now;
        }

        [StringLength(32)]
        public sealed override string Id { get; set; }

        public Role Role { get; set; }

        [StringLength(250, ErrorMessage = "Characters must be less than 250")]
        public string Description { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
