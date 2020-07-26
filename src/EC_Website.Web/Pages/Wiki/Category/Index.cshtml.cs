using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces.Repositories;

namespace EC_Website.Web.Pages.Wiki.Category
{
    public class CategoriesIndexModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public CategoriesIndexModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        public Core.Entities.WikiModel.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categorySlug = RouteData.Values["slug"].ToString();

            if (categorySlug == null)
            {
                return NotFound();
            }

            Category = await _wikiRepository.GetAsync<Core.Entities.WikiModel.Category>(i => i.Slug == categorySlug);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
