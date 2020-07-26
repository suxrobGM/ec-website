using System.Collections.Generic;
using System.Threading.Tasks;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task UpdateUserRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task UpdateUserBadgesAsync(ApplicationUser user, IEnumerable<string> badges);

        /// <summary>
        /// Deeply deletes all records of the user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task DeleteDeeplyAsync(ApplicationUser user);
    }
}
