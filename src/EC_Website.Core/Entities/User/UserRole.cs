using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Core.Entities.User
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Moderator,
        Developer,
        Editor
    }

    public class UserRole : IdentityRole
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

        public Role Role { get; set; }
        
        [StringLength(250, ErrorMessage = "Characters must be less than 250")]
        public string Description { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
