using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Pages.Wiki
{
    public class WikiArticleIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public WikiArticleIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsMainPage { get; set; }
        public WikiArticle Article { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            Article = await _context.WikiArticles.Where(i => i.Slug == articleSlug).FirstOrDefaultAsync();

            if (Article == null && articleSlug == "Economic_Crisis_Wiki")
            {
                return RedirectToPage("/Wiki/Create", new { firstMainPage = true });
            }
            else if (Article == null)
            {
                return NotFound($"Wiki page with slug '{articleSlug}' does not found");
            }

            if (articleSlug == "Economic_Crisis_Wiki")
            {
                IsMainPage = true;
            }

            return Page();
        }
    }
}
