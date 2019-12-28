using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.Article
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class DeleteArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Blog.BlogEntry Entry { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entry = await _context.BlogEntries
                .Include(b => b.Author).FirstOrDefaultAsync(m => m.Id == id);


            if (Entry == null)
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

            Entry = await _context.BlogEntries.FindAsync(id);

            if (Entry != null)
            {
                _context.BlogEntries.Remove(Entry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
