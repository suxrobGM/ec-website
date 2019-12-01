using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public WikiArticle Article { get; set; }

        public IActionResult OnGet()
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            Article = _context.WikiArticles.Where(i => i.Slug == articleSlug).FirstOrDefault();

            if (Article == null && articleSlug == "Main_Page")
            {
                return RedirectToPage("/Wiki/Create");
            }
            else if (Article == null)
            {
                return NotFound($"Wiki page with slug '{articleSlug}' does not found");
            }

            return Page();
        }
    }
}
