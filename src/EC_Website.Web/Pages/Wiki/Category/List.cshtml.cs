using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class CategoriesListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoriesListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Core.Entities.Wikipedia.Category> Categories { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.WikiCategories.ToListAsync();

            return Page();
        }
    }
}
