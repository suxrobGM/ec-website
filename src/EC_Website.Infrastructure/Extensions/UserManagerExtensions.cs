using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Infrastructure.Extensions;

public static class UserManagerExtensions
{
    /// <summary>
    /// Compare equality of the roles of the two users
    /// </summary>
    /// <param name="userManager">Object of the UserManager class</param>
    /// <param name="firstUserClaims">Claims of the User1</param>
    /// <param name="secondUser">User2</param>
    /// <returns>True if user1 role is lower or equal to the user2 role, otherwise returns false</returns>
    public static async Task<bool> CheckRoleLowerOrEqualAsync(this UserManager<ApplicationUser> userManager,
        ClaimsPrincipal firstUserClaims, ApplicationUser secondUser)
    {
        var user1 = await userManager.GetUserAsync(firstUserClaims);
        return await userManager.CheckRoleLowerOrEqualAsync(user1, secondUser);
    }

    /// <summary>
    /// Compare equality of the roles of the two users
    /// </summary>
    /// <param name="userManager">Object of the UserManager class</param>
    /// <param name="user1">User1</param>
    /// <param name="user2">User2</param>
    /// <returns>True if user1 role is lower or equal to the user2 role, otherwise returns false</returns>
    public static async Task<bool> CheckRoleLowerOrEqualAsync(this UserManager<ApplicationUser> userManager, 
        ApplicationUser user1, ApplicationUser user2)
    {
        var lowerOrEqualRole = false;
        var rolesUser1 = await userManager.GetRolesAsync(user1);
        var rolesUser2 = await userManager.GetRolesAsync(user2);

        // Is same user
        if (user1.Id == user2.Id)
        {
            return false;
        }

        // First user have some roles but second user does not have any roles
        if (rolesUser1.Count != 0 && rolesUser2.Count == 0)
        {
            return false;
        }

        // SuperAdmin always can access to everything
        if (rolesUser1.Contains("SuperAdmin"))
        {
            return false;
        }

        if (rolesUser1.Contains("Admin") && 
            (rolesUser2.Contains("SuperAdmin") ||
             rolesUser2.Contains("Admin")))
        {
            lowerOrEqualRole = true;
        }
        else if (rolesUser1.Contains("Moderator") && 
                 (rolesUser2.Contains("SuperAdmin") ||
                  rolesUser2.Contains("Admin") || 
                  rolesUser2.Contains("Moderator")))
        {
            lowerOrEqualRole = true;
        }
        else if (rolesUser1.Contains("Editor") && 
                 (rolesUser2.Contains("SuperAdmin") ||
                  rolesUser2.Contains("Admin") || 
                  rolesUser2.Contains("Moderator") ||
                  rolesUser2.Contains("Editor")))
        {
            lowerOrEqualRole = true;
        }
        else if (rolesUser1.Contains("Developer") && 
                 (rolesUser2.Contains("SuperAdmin") ||
                  rolesUser2.Contains("Admin") || 
                  rolesUser2.Contains("Moderator") ||
                  rolesUser2.Contains("Editor") ||
                  rolesUser2.Contains("Developer")))
        {
            lowerOrEqualRole = true;
        }

        return lowerOrEqualRole;
    }
}