using System.Threading.Tasks;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Entities.User;

namespace EC_Website.Core.Interfaces
{
    public interface IForumRepository : IRepository
    {
        Task AddFavoriteThreadAsync(Thread favoriteThread, ApplicationUser user);
        Task DeleteForumAsync(ForumHead forum);
        Task DeleteBoardAsync(Board board, bool saveChanges = true);
        Task DeleteThreadAsync(Thread thread, bool saveChanges = true);
        Task DeleteFavoriteThreadAsync(Thread favoriteThread);
        Task DeletePostAsync(Post post);
    }
}