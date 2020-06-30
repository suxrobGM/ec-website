using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.Blog;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Article
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class DeleteArticleModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly IWebHostEnvironment _env;

        public DeleteArticleModel(IRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [BindProperty]
        public BlogEntry Entry { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entry = await _repository.GetByIdAsync<BlogEntry>(id);

            if (Entry == null)
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

            Entry = await _repository.GetByIdAsync<BlogEntry>(id);

            if (Entry != null)
            {
                await _repository.DeleteAsync(Entry);
                ImageHelper.RemoveImage(Entry.CoverPhotoPath, _env);
            }

            return RedirectToPage("/Index");
        }
    }
}
