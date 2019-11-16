using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.Wiki.Category
{
    [Authorize]
    public class CategoriesListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Wikipedia.Category> Categories { get;set; }

        public async Task OnGetAsync()
        {
            Categories = await _context.WikiCategories.ToListAsync();
        }
    }
}
