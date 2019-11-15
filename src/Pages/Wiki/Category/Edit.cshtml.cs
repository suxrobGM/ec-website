using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Data;

namespace EC_Website.Pages.Wiki.Category
{
    public class EditCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string id)
        {
            Category = _context.WikiCategories.Where(i => i.Id == id).First();
            return Page();
        }

        [BindProperty]
        public Models.Wikipedia.Category Category { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var category = _context.WikiCategories.Where(i => i.Id == Category.Id).First();
            category.Name = Category.Name;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Wiki/Index");
        }
    }
}
