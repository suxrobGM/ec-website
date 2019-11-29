using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Data;

namespace EC_Website.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class CreateCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
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

            Category.GenerateUrl();
            _context.WikiCategories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Wiki/Index");
        }
    }
}
