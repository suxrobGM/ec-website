using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EC_WebSite.Models.UserModel;

namespace EC_WebSite.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool InOneOfTheseRoles(this ClaimsPrincipal claims, Role startRange, Role endRange = Role.SuperAdmin)
        {
            bool hasRole = false;
            for (int i = (int)startRange; i <= (int)endRange; i++)
            {
                string roleName = Enum.GetName(typeof(Role), i);
                if (claims.IsInRole(roleName))
                    hasRole = true;
            }
            return hasRole;
        }
    }
}
