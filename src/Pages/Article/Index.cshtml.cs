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
        private readonly ApplicationDbContext _db;

        public ArticleIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
       
        public Models.Blog.BlogArticle Article { get; set; }
        public PaginatedList<Comment> Comments { get; set; }
        public string[] ArticleTags { get; set; }

        [BindProperty]
        public string CommentText { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            string articleUrl = RouteData.Values["articleUrl"].ToString();
            Article = _db.BlogArticles.Where(i => i.Url == articleUrl).First();
            Comments = PaginatedList<Comment>.Create(Article.Comments, pageIndex);
            ArticleTags = Article.Tags.Split(',');

            if (!Request.Headers["User-Agent"].ToString().ToLower().Contains("bot"))
            {
                Article.ViewCount++;
            }

            await _db.SaveChangesAsync();
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

            var article = _db.BlogArticles.Where(i => i.Url == articleUrl).First();
            var author = _db.Users.Where(i => i.UserName == userName).First();
            var comment = new Comment()
            {
                Author = author,
                Text = CommentText,
            };
            article.Comments.Add(comment);

            await _db.SaveChangesAsync();
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
            
            var comment = _db.Comments.Where(i => i.Id == commentId).First();
            var author = _db.Users.Where(i => i.UserName == userName).First();
            var commentReply = new Comment()
            {
                Author = author,
                Article = comment.Article,
                Text = CommentText
            };
            comment.Replies.Add(commentReply);

            await _db.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber } ,commentId);
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(string commentId, string rootCommentId)
        {
            var articleUrl = RouteData.Values["articleUrl"].ToString();
            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out int pageNumber))
            {
                pageNumber = 1;
            }
            var comment = _db.Comments.Where(i => i.Id == commentId).First();

            await RemoveChildrenCommentsAsync(comment);
            _db.Comments.Remove(comment);

            await _db.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber }, rootCommentId);
        }

        private async Task RemoveChildrenCommentsAsync(Comment comment)
        {
            foreach (var reply in comment.Replies)
            {
                await RemoveChildrenCommentsAsync(reply);
                _db.Comments.Remove(reply);
            }
        }
    }
}