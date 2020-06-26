using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Core.Entities.Blog;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Article
{
    public class ArticleIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ArticleIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public BlogEntry Entry { get; set; }
        public PaginatedList<Comment> Comments { get; set; }

        [BindProperty]
        public string CommentContent { get; set; }
        public string ArticleTags { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1, bool increaseViewCount = true)
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            Entry = await _context.BlogEntries.FirstOrDefaultAsync(i => i.Slug == articleSlug);

            if (Entry == null)
            {
                return NotFound();
            }

            Comments = PaginatedList<Comment>.Create(Entry.Comments, pageIndex);
            ArticleTags = string.Join(',', Entry.Tags);

            if (increaseViewCount && !Request.Headers["User-Agent"].ToString().ToLower().Contains("bot"))
            {
                Entry.ViewCount++;
            }

            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync()
        {
            var articleSlug = RouteData.Values["slug"].ToString();

            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out var pageNumber))
            {
                pageNumber = 1;
            }

            if (string.IsNullOrWhiteSpace(CommentContent))
            {
                ModelState.AddModelError("CommentContent", "Empty comment content");
                return Page();
            }

            var article = await _context.BlogEntries.FirstAsync(i => i.Slug == articleSlug);
            var author = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);
            var comment = new Comment()
            {
                Author = author,
                Content = CommentContent,
            };
            article.Comments.Add(comment);

            await _context.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber }, comment.Id);
        }

        public async Task<IActionResult> OnPostReplyToCommentAsync(string commentId)
        {
            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out var pageNumber))
            {
                pageNumber = 1;
            }

            if (string.IsNullOrWhiteSpace(CommentContent))
            {
                ModelState.AddModelError("CommentContent", "Empty comment content");
                return Page();
            }
            
            var comment = await _context.Comments.FirstAsync(i => i.Id == commentId);
            var author = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);
            var commentReply = new Comment()
            {
                Author = author,
                Entry = comment.Entry,
                Content = CommentContent
            };
            comment.Replies.Add(commentReply);

            await _context.SaveChangesAsync();
            return RedirectToPage("", "", new { pageIndex = pageNumber } ,commentId);
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(string commentId, string rootCommentId)
        {
            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out var pageNumber))
            {
                pageNumber = 1;
            }
            var comment = await _context.Comments.FirstAsync(i => i.Id == commentId);

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