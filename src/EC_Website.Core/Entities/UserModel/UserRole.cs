using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;
using EC_Website.Core.Interfaces.Entities;

namespace EC_Website.Core.Entities.UserModel
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Moderator,
        Editor,
        Developer
    }

    public class UserRole : IdentityRole, IEntity<string>
    {
        public UserRole() : this(Role.Developer)
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
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
