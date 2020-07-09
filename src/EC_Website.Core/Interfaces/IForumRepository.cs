using System.Threading.Tasks;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces
{
    public interface IForumRepository : IRepository
    {
        Task AddThreadAsync(Thread thread);
        Task AddFavoriteThreadAsync(Thread favoriteThread, ApplicationUser user);
        Task UpdateThreadAsync(Thread thread);
        Task DeleteForumAsync(Forum forum);
        Task DeleteBoardAsync(Board board, bool saveChanges = true);
        Task DeleteThreadAsync(Thread thread, bool saveChanges = true);
        Task DeleteFavoriteThreadAsync(Thread favoriteThread, ApplicationUser user);
        Task DeletePostAsync(Post post);
    }
}