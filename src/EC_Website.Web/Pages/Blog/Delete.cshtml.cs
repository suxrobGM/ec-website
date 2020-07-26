using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Blog
{
    [Authorize(Policy = Policies.CanManageBlogs)]
    public class DeleteModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ImageHelper _imageHelper;

        public DeleteModel(IBlogRepository blogRepository, ImageHelper imageHelper)
        {
            _blogRepository = blogRepository;
            _imageHelper = imageHelper;
        }

        [BindProperty]
        public Core.Entities.BlogModel.Blog Blog { get; set; }
        public string Tags { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _blogRepository.GetByIdAsync<Core.Entities.BlogModel.Blog>(id);

            if (Blog == null)
            {
                return NotFound();
            }

            Tags = Tag.JoinTags(Blog.BlogTags.Select(i => i.Tag));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _blogRepository.GetByIdAsync<Core.Entities.BlogModel.Blog>(id);

            if (Blog != null)
            {
                await _blogRepository.DeleteBlogAsync(Blog);
                _imageHelper.RemoveImage(Blog.CoverPhotoPath);
            }

            return RedirectToPage("/Index");
        }
    }
}
