using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class DeleteCategoryModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public DeleteCategoryModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        [BindProperty]
        public Core.Entities.WikiModel.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _wikiRepository.GetByIdAsync<Core.Entities.WikiModel.Category>(id);

            if (Category == null)
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

            Category = await _wikiRepository.GetByIdAsync<Core.Entities.WikiModel.Category>(id);
            await _wikiRepository.DeleteAsync(Category);
            return RedirectToPage("./List");
        }
    }
}
