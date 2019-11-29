using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public void OnGet()
        {
            var categoryUrl = RouteData.Values["categoryUrl"].ToString();
            Category = _context.WikiCategories.Where(i => i.Url == categoryUrl).First();
        }
    }
}
