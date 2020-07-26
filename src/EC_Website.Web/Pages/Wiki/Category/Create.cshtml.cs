using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class CreateCategoryModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public CreateCategoryModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            return Page();
        }

        [BindProperty]
        public Core.Entities.WikiModel.Category Category { get; set; }
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= "./List";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category.Slug = Category.Name.Slugify(false,false);
            await _wikiRepository.AddAsync(Category);
            return RedirectToPage(returnUrl);
        }
    }
}
