using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class EditCategoryModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public EditCategoryModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

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

        [BindProperty]
        public Core.Entities.WikiModel.Category Category { get; set; }
        
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = await _wikiRepository.GetByIdAsync<Core.Entities.WikiModel.Category>(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = Category.Name;
            Category.Slug = Category.Name.Slugify(false,false);
            await _wikiRepository.UpdateAsync(category);
            return RedirectToPage("./List");
        }
    }
}
