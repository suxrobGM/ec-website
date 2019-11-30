using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;
using EC_Website.Models.Blog;

namespace EC_Website.Pages.Article
{
    public class ArticleIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ArticleIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public BlogArticle Article { get; set; }
        public PaginatedList<Comment> Comments { get; set; }
        public string[] ArticleTags { get; set; }

        [BindProperty]
        public string CommentText { get; set; }

        public async Task OnGetAsync(int pageIndex = 1, bool increaseViewCount = true)
        {
            string articleUrl = RouteData.Values["articleUrl"].ToString();
            Article = _context.BlogArticles.Where(i => i.Url == articleUrl).First();
            Comments = PaginatedList<Comment>.Create(Article.Comments, pageIndex);
            ArticleTags = Article.Tags.Split(',');

            if (increaseViewCount && !Request.Headers["User-Agent"].ToString().ToLower().Contains("bot"))
            {
                Article.ViewCount++;
            }

            await _context.SaveChangesAsync();
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
            await OnGetAsync(pageIndex, false);
            return Page();
        }

        public async Task<IActionResult> OnGetUnlikesArticleAsync(string articleId, int pageIndex)
        {
            var article = _context.BlogArticles.Where(i => i.Id == articleId).First();
            var userLikedArticle = article.UsersLiked.Where(i => i.ArticleId == articleId).FirstOrDefault();
            article.UsersLiked.Remove(userLikedArticle);

            await _context.SaveChangesAsync();
            await OnGetAsync(pageIndex, false);
            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync()
        {
            var articleUrl = RouteData.Values["articleUrl"].ToString();
            var userName = User.Identity.Name;

            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out int pageNumber))
            {
                pageNumber = 1;
            }

            if (string.IsNullOrWhiteSpace(CommentText))
            {
                ModelState.AddModelError("CommentText", "Empty comment text");
                return Page();
            }

            var article = _context.BlogArticles.Where(i => i.Url == articleUrl).First();
            var author = _context.Users.Where(i => i.UserName == userName).First();
            var comment = new Comment()
            {
                Author = author,
                Text = CommentText,
            };
            article.Comments.Add(comment);

            await _context.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber }, comment.Id);
        }

        public async Task<IActionResult> OnPostReplyToCommentAsync(string commentId)
        {
            var articleUrl = RouteData.Values["articleUrl"].ToString();
            var userName = User.Identity.Name;

            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out int pageNumber))
            {
                pageNumber = 1;
            }

            if (string.IsNullOrWhiteSpace(CommentText))
            {
                ModelState.AddModelError("CommentText", "Empty comment text");
                return Page();
            }
            
            var comment = _context.Comments.Where(i => i.Id == commentId).First();
            var author = _context.Users.Where(i => i.UserName == userName).First();
            var commentReply = new Comment()
            {
                Author = author,
                Article = comment.Article,
                Text = CommentText
            };
            comment.Replies.Add(commentReply);

            await _context.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber } ,commentId);
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(string commentId, string rootCommentId)
        {
            var articleUrl = RouteData.Values["articleUrl"].ToString();
            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out int pageNumber))
            {
                pageNumber = 1;
            }
            var comment = _context.Comments.Where(i => i.Id == commentId).First();

            await RemoveChildrenCommentsAsync(comment);
            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber }, rootCommentId);
        }

        private async Task RemoveChildrenCommentsAsync(Comment comment)
        {
            foreach (var reply in comment.Replies)
            {
                await RemoveChildrenCommentsAsync(reply);
                _context.Comments.Remove(reply);
            }
        }
    }
}