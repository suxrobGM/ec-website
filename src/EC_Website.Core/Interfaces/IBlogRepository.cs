using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Interfaces
{
    public interface IBlogRepository : IRepository
    {
        Task AddTagsAsync(Blog blog, params Tag[] tags);
        Task AddLikeAsync(Blog blog, ApplicationUser user);
        Task RemoveLikeAsync(Blog blog, ApplicationUser user);
        Task RemoveTagsAsync(Blog blog, params Tag[] tags);
    }
}
