using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;
using System.Linq;
using EC_Website.Models.Blog;
using System.Threading.Tasks;

namespace EC_Website.Pages.Home
{
    public class HomeIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HomeIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<BlogArticle> Articles { get; set; }

        public void OnGet(int pageIndex = 1)
        {
            Articles = PaginatedList<BlogArticle>.Create(_context.BlogArticles, pageIndex);
        }

        public async Task<IActionResult> OnGetLikesArticleAsync(string articleId, int pageIndex)
        {
            var article = _context.BlogArticles.Where(i => i.Id == articleId).First();
            var user = _context.Users.Where(i => i.UserName == User.Identity.Name).First();
            article.UsersLiked.Add(new UserLikedBlogArticle()
            {
                Article = article,
                ArticleId = articleId,
                User = user,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            OnGet(pageIndex);
            return Page();
        }

        public async Task<IActionResult> OnGetUnlikesArticleAsync(string articleId, int pageIndex)
        {
            var article = _context.BlogArticles.Where(i => i.Id == articleId).First();
            var userLikedArticle = article.UsersLiked.Where(i => i.ArticleId == articleId).FirstOrDefault();
            article.UsersLiked.Remove(userLikedArticle);

            await _context.SaveChangesAsync();
            OnGet(pageIndex);
            return Page();
        }
    }
}