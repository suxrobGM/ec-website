using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EC_WebSite.Models
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Moderator,
        Developer,
        Special
    }

    public class UserRole : IdentityRole
    {
        public UserRole(Role role): base(role.ToString())
        {
           
        }

        public Role Role { get; set; }
    }
}
