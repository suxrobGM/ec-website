using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Wiki.Category
{
    public class CategoriesIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Core.Entities.Wikipedia.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categorySlug = RouteData.Values["slug"].ToString();

            if (categorySlug == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.FirstOrDefaultAsync(i => i.Slug == categorySlug);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
