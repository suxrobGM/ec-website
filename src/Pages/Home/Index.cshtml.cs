using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;

namespace EC_Website.Pages.Home
{
    public class HomeIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public HomeIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public PaginatedList<Models.Blog.BlogArticle> Articles { get; set; }

        public IActionResult OnGet(int pageIndex = 1)
        {
            Articles = PaginatedList<Models.Blog.BlogArticle>.Create(_db.BlogArticles, pageIndex);

            return Page();
        }
    }
}