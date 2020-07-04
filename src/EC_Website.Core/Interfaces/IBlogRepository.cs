using System.Threading.Tasks;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces
{
    public interface IBlogRepository : IRepository
    {
        Task AddTagsAsync(Blog blog, bool saveChanges = true, params Tag[] tags);
        Task AddLikeAsync(Blog blog, ApplicationUser user);
        Task DeleteTagsAsync(Tag[] tags);
        Task RemoveLikeAsync(Blog blog, ApplicationUser user);
        Task RemoveBlogTagsAsync(Blog blog, Tag[] tags);
    }
}
