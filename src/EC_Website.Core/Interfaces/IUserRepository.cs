using System.Threading.Tasks;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces
{
    public interface IUserRepository : IRepository
    {
        /// <summary>
        /// Deeply deletes all records of the user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task DeleteDeeplyAsync(ApplicationUser user);
    }
}
