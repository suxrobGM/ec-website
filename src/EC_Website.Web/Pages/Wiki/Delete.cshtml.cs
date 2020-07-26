using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class DeleteWikiArticleModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public DeleteWikiArticleModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        [BindProperty]
        public WikiPage WikiPage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiPage = await _wikiRepository.GetByIdAsync<WikiPage>(id);

            if (WikiPage == null)
            {
                return NotFound();
            }

            if (WikiPage.Slug == "Economic_Crisis_Wiki" && !User.IsInRole("SuperAdmin"))
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

            WikiPage = await _wikiRepository.GetByIdAsync<WikiPage>(id);
            await _wikiRepository.DeleteAsync(WikiPage);
            return RedirectToPage("./Index", new { slug = "Economic_Crisis_Wiki" });
        }
    }
}
