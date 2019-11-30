using System;
using System.Security.Claims;
using EC_Website.Models.UserModel;

namespace EC_Website.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasMinimumRole(this ClaimsPrincipal claims, Role highLevel, Role lowLevel = Role.SuperAdmin)
        {
            bool hasRole = false;
            for (int i = (int)highLevel; i <= (int)lowLevel; i++)
            {
                string roleName = Enum.GetName(typeof(Role), i);
                if (claims.IsInRole(roleName))
                    hasRole = true;
            }
            return hasRole;
        }
    }
}
