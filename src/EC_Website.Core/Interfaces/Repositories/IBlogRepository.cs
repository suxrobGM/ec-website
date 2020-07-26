using System.Threading.Tasks;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces.Repositories
{
    public interface IBlogRepository : IRepository
    {
        Task AddBlogAsync(Blog blog);
        Task AddCommentAsync(Blog blog, Comment comment);
        Task AddReplyToCommentAsync(Comment parentComment, Comment childComment);
        Task UpdateBlogAsync(Blog blog);
        Task UpdateTagsAsync(Blog blog, bool saveChanges = true, params Tag[] tags);
        Task AddLikeAsync(Blog blog, ApplicationUser user);
        Task RemoveLikeAsync(Blog blog, ApplicationUser user);
        Task DeleteBlogAsync(Blog blog);
        Task DeleteCommentAsync(Comment comment, bool saveChanges = true);
    }
}
