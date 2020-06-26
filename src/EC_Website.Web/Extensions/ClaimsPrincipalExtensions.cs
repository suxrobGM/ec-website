using System;
using System.Security.Claims;
using EC_Website.Core.Entities.User;

namespace EC_Website.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasMinimumRole(this ClaimsPrincipal claims, Role lowLevel, Role highLevel = Role.SuperAdmin)
        {
            var hasRole = false;
            for (var i = (int)highLevel; i <= (int)lowLevel; i++)
            {
                var roleName = Enum.GetName(typeof(Role), i);
                if (claims.IsInRole(roleName))
                    hasRole = true;
            }
            return hasRole;
        }
    }
}
