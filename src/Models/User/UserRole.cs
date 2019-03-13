using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Utils;
using Microsoft.AspNetCore.Identity;

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
            Id = GeneratorId.GenerateShort();
            Role = role;            
        }

        public Role Role { get; set; }        
    }
}
