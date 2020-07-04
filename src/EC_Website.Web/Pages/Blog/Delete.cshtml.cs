using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Blog
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class DeleteModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _env;

        public DeleteModel(IBlogRepository blogRepository, IWebHostEnvironment env)
        {
            _blogRepository = blogRepository;
            _env = env;
        }

        [BindProperty]
        public Core.Entities.BlogModel.Blog Blog { get; set; }
        public string Tags { get; set; } // TODO

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _blogRepository.GetByIdAsync<Core.Entities.BlogModel.Blog>(id);
            Tags = string.Join(',', Blog.BlogTags.SelectMany(i => i.Tag.Name));

            if (Blog == null)
            {
                return NotFound();
            }
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
                await _blogRepository.DeleteAsync(Blog);
                ImageHelper.RemoveImage(Blog.CoverPhotoPath, _env);
            }

            return RedirectToPage("/Index");
        }
    }
}
