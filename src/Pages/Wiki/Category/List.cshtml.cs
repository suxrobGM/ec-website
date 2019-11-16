using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.Wiki.Category
{
    public class CategoriesListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Wikipedia.Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.WikiCategories.ToListAsync();
        }
    }
}
