using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;

namespace EC_Website.Web.Pages.Blog
{
    public class AjaxModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AjaxModel(IBlogRepository blogRepository,
            UserManager<ApplicationUser> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnGetLikeArticleAsync(string blogId)
        {
            var blog = await _blogRepository.GetByIdAsync<Core.Entities.BlogModel.Blog>(blogId);
            var user = await _userManager.GetUserAsync(User);
            
            if (string.IsNullOrEmpty(blogId) || blog == null)
            {
                return BadRequest($"Specified blog with {blogId} could not be found");
            }

            var alreadyLiked = blog.LikedUsers.Any(i => i.UserId == user.Id);

            if (alreadyLiked)
            {
                await _blogRepository.RemoveLikeAsync(blog, user);
            }
            else
            {
                await _blogRepository.AddLikeAsync(blog, user);
            }

            return new OkObjectResult(blog.LikedUsers.Count);
        }
    }
}