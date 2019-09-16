using System;
using System.Security.Claims;
using EC_Website.Models.UserModel;

namespace EC_Website.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasOneOfTheseRoles(this ClaimsPrincipal claims, Role startRange, Role endRange = Role.SuperAdmin)
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
