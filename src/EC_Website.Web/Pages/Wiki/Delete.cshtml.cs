using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Core.Entities.Wikipedia;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class DeleteWikiArticleModel : PageModel
    {
        private readonly IRepository _repository;

        public DeleteWikiArticleModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public WikiEntry WikiEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiEntry = await _repository.GetByIdAsync<WikiEntry>(id);

            if (WikiEntry == null)
            {
                return NotFound();
            }

            if (WikiEntry.Slug == "Economic_Crisis_Wiki" && !User.IsInRole("SuperAdmin"))
            {
                return BadRequest("Only SuperAdmin can delete wiki main page");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiEntry = await _repository.GetByIdAsync<WikiEntry>(id);
            await _repository.DeleteAsync(WikiEntry);
            return RedirectToPage("./Index", new { slug = "Economic_Crisis_Wiki" });
        }
    }
}
