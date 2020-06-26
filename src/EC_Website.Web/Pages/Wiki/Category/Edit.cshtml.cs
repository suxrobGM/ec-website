using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class EditCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.FirstOrDefaultAsync(i => i.Id == id);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        [BindProperty]
        public Core.Entities.Wikipedia.Category Category { get; set; }
        
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = await _context.WikiCategories.FirstAsync(i => i.Id == id);
            category.Name = Category.Name;
            Category.Slug = ArticleBase.CreateSlug(Category.Name, false, false);
            await _context.SaveChangesAsync();

            return RedirectToPage("./List");
        }
    }
}
