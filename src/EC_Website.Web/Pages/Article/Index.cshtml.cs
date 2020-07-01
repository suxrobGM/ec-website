using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Core.Entities.Blog;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Article
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IRepository repository,
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
       
        public BlogEntry Entry { get; set; }
        public PaginatedList<Comment> Comments { get; set; }

        [BindProperty]
        public string CommentContent { get; set; }
        public string ArticleTags { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1, bool increaseViewCount = true)
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            Entry = await _repository.GetAsync<BlogEntry>(i => i.Slug == articleSlug);

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

            await _repository.UpdateAsync(Entry);
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

            var article = await _repository.GetAsync<BlogEntry>(i => i.Slug == articleSlug);
            var author = await _userManager.GetUserAsync(User);
            var comment = new Comment()
            {
                Author = author,
                Content = CommentContent,
            };
            article.Comments.Add(comment);

            await _repository.UpdateAsync(article);
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
            
            var comment = await _repository.GetByIdAsync<Comment>(commentId);
            var author = await _userManager.GetUserAsync(User);
            var commentReply = new Comment()
            {
                Author = author,
                Entry = comment.Entry,
                Content = CommentContent
            };
            comment.Replies.Add(commentReply);

            await _repository.UpdateAsync(comment);
            return RedirectToPage("", "", new { pageIndex = pageNumber } ,commentId);
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(string commentId, string rootCommentId)
        {
            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out var pageNumber))
            {
                pageNumber = 1;
            }
            var comment = await _repository.GetByIdAsync<Comment>(commentId);

            await RemoveChildrenCommentsAsync(comment);
            await _repository.DeleteAsync(comment);
            return RedirectToPage("", "", new { pageIndex = pageNumber }, rootCommentId);
        }

        private async Task RemoveChildrenCommentsAsync(Comment comment)
        {
            foreach (var reply in comment.Replies)
            {
                await RemoveChildrenCommentsAsync(reply);
                await _repository.DeleteAsync(reply);
            }
        }
    }
}