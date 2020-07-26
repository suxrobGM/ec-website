using System.Threading.Tasks;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces.Repositories
{
    public interface IForumRepository : IRepository
    {
        Task AddBoardAsync(Board board);
        Task AddThreadAsync(Thread thread);
        Task AddFavoriteThreadAsync(Thread thread, ApplicationUser user);
        Task RemoveFavoriteThreadAsync(Thread thread, ApplicationUser user);
        Task UpdateBoardAsync(Board board);
        Task UpdateThreadAsync(Thread thread);
        Task DeleteForumAsync(Forum forum);
        Task DeleteBoardAsync(Board board, bool saveChanges = true);
        Task DeleteThreadAsync(Thread thread, bool saveChanges = true);
        Task DeletePostAsync(Post post);
    }
}