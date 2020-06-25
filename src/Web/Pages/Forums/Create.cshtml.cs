using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Models.ForumModel;
using EC_Website.Data;

namespace EC_Website.Pages.Forums
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string ForumTitle { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _context.ForumHeads.AddAsync(new ForumHead() { Title = ForumTitle });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}