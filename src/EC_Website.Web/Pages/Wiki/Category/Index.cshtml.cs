using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki.Category
{
    public class CategoriesIndexModel : PageModel
    {
        private readonly IRepository _repository;

        public CategoriesIndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public Core.Entities.Wikipedia.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categorySlug = RouteData.Values["slug"].ToString();

            if (categorySlug == null)
            {
                return NotFound();
            }

            Category = await _repository.GetAsync<Core.Entities.Wikipedia.Category>(i => i.Slug == categorySlug);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
