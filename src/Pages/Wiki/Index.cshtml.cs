using System.Linq;
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

        public void OnGet()
        {
            //var articleUrl = RouteData.Values["wikiArticleUrl"].ToString();
            //Article = _context.WikiArticles.Where(i => i.Url.Contains(articleUrl)).First();
        }
    }
}
