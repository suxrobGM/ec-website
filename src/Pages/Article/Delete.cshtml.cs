using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.Article
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DeleteArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Blog.BlogArticle Article { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.BlogArticles
                .Include(b => b.Author).FirstOrDefaultAsync(m => m.Id == id);


            if (Article == null)
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

            Article = await _context.BlogArticles.FindAsync(id);

            if (Article != null)
            {
                _context.BlogArticles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Home/Index");
        }
    }
}
