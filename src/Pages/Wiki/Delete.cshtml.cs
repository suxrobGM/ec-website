using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class DeleteWikiArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteWikiArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WikiArticle WikiArticle { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiArticle = await _context.WikiArticles
                .Include(w => w.Author).FirstOrDefaultAsync(m => m.Id == id);

            if (WikiArticle == null)
            {
                return NotFound();
            }

            if (WikiArticle.Slug == "Economic_Crisis_Wiki" && !User.IsInRole("SuperAdmin"))
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

            WikiArticle = await _context.WikiArticles.FindAsync(id);

            if (WikiArticle != null)
            {
                _context.WikiArticles.Remove(WikiArticle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { slug = "Main_Page" });
        }
    }
}
