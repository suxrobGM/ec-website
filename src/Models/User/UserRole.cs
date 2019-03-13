using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.UserModel
{
    public enum Role
    {
        Editor,
        Developer,
        Moderator,
        Admin,
        SuperAdmin                              
    }

    public class UserRole : IdentityRole
    {
        public UserRole(Role role): base(role.ToString())
        {
            Id = GeneratorId.GenerateLong();
            Role = role;            
        }

        public Role Role { get; set; }        
    }
}
