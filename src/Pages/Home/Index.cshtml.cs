using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;
using EC_Website.Models.Blog;

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

        public IActionResult OnGet(int pageIndex = 1)
        {
            Articles = PaginatedList<BlogArticle>.Create(_context.BlogArticles, pageIndex);

            return Page();
        }

        public async Task<IActionResult> OnGetLikesArticleAsync(string id, int pageIndex)
        {
            var article = await _context.BlogArticles.FirstAsync(i => i.Id == id);
            var user = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);
            article.UsersLiked.Add(new UserLikedBlogArticle()
            {
                Article = article,
                ArticleId = article.Id,
                User = user,
                UserId = user.Id
            });

            await _context.SaveChangesAsync();
            OnGet(pageIndex);
            return Page();
        }

        public async Task<IActionResult> OnGetUnlikesArticleAsync(string id, int pageIndex)
        {
            var article = await _context.BlogArticles.FirstAsync(i => i.Id == id);
            var userLikedArticle = article.UsersLiked.FirstOrDefault(i => i.ArticleId == id);
            article.UsersLiked.Remove(userLikedArticle);

            await _context.SaveChangesAsync();
            OnGet(pageIndex);
            return Page();
        }
    }
}