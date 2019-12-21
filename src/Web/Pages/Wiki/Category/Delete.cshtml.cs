using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class DeleteCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Wikipedia.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.FindAsync(id);

            if (Category != null)
            {
                _context.WikiCategories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./List");
        }
    }
}
