using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.Wiki.Category
{
    public class CategoriesIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Wikipedia.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categorySlug = RouteData.Values["slug"].ToString();

            if (categorySlug == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.Where(i => i.Slug == categorySlug).FirstOrDefaultAsync();

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
