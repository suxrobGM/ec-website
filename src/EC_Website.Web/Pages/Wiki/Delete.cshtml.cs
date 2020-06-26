using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.Wikipedia;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class DeleteWikiArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteWikiArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WikiEntry WikiEntry { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiEntry = await _context.WikiEntries
                .Include(w => w.Author).FirstOrDefaultAsync(m => m.Id == id);

            if (WikiEntry == null)
            {
                return NotFound();
            }

            if (WikiEntry.Slug == "Economic_Crisis_Wiki" && !User.IsInRole("SuperAdmin"))
            {
                return NotFound("Only SuperAdmin can delete wiki main page");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiEntry = await _context.WikiEntries.FindAsync(id);

            if (WikiEntry != null)
            {
                _context.WikiEntries.Remove(WikiEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { slug = "Economic_Crisis_Wiki" });
        }
    }
}
