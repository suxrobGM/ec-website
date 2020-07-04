using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IBlogRepository blogRepository,
            UserManager<ApplicationUser> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }
       
        public Core.Entities.BlogModel.Blog Blog { get; set; }
        public PaginatedList<Comment> Comments { get; set; }

        [BindProperty]
        public string CommentContent { get; set; }
        public string ArticleTags { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1, bool increaseViewCount = true)
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            Blog = await _blogRepository.GetAsync<Core.Entities.BlogModel.Blog>(i => i.Slug == articleSlug);

            if (Blog == null)
            {
                return NotFound();
            }

            Comments = PaginatedList<Comment>.Create(Blog.Comments, pageIndex);
            ArticleTags = string.Join(',', Blog.BlogTags.SelectMany(i => i.Tag.Name));

            if (increaseViewCount && !Request.Headers["User-Agent"].ToString().ToLower().Contains("bot"))
            {
                Blog.ViewCount++;
            }

            await _blogRepository.UpdateAsync(Blog);
            return Page();
        }

        // BUG 
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

            var article = await _blogRepository.GetAsync<Core.Entities.BlogModel.Blog>(i => i.Slug == articleSlug);
            var author = await _userManager.GetUserAsync(User);
            var comment = new Comment()
            {
                Author = author,
                Content = CommentContent,
            };
            article.Comments.Add(comment);

            await _blogRepository.UpdateAsync(article);
            return RedirectToPage("", "", new { pageIndex = pageNumber }, comment.Id);
        }

        // BUG
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
            
            var comment = await _blogRepository.GetByIdAsync<Comment>(commentId);
            var author = await _userManager.GetUserAsync(User);
            var commentReply = new Comment()
            {
                Author = author,
                Blog = comment.Blog,
                Content = CommentContent
            };
            comment.Replies.Add(commentReply);

            await _blogRepository.UpdateAsync(comment);
            return RedirectToPage("", "", new { pageIndex = pageNumber } ,commentId);
        }

        // BUG
        public async Task<IActionResult> OnPostDeleteCommentAsync(string commentId, string rootCommentId)
        {
            if (!int.TryParse(HttpContext.Request.Query["pageIndex"].ToString(), out var pageNumber))
            {
                pageNumber = 1;
            }
            var comment = await _blogRepository.GetByIdAsync<Comment>(commentId);

            await RemoveChildrenCommentsAsync(comment);
            await _blogRepository.DeleteAsync(comment);
            return RedirectToPage("", "", new { pageIndex = pageNumber }, rootCommentId);
        }

        // BUG
        private async Task RemoveChildrenCommentsAsync(Comment comment)
        {
            foreach (var reply in comment.Replies)
            {
                await RemoveChildrenCommentsAsync(reply);
                await _blogRepository.DeleteAsync(reply);
            }
        }
    }
}