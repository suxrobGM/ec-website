using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize]
    public class EditPostModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly IAuthorizationService _authorization;

        public EditPostModel(IForumRepository forumRepository, IAuthorizationService authorization)
        {
            _forumRepository = forumRepository;
            _authorization = authorization;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(string postId)
        {
            if (postId == null)
            {
                return NotFound();
            }

            Post = await _forumRepository.GetByIdAsync<Post>(postId);

            if (Post == null)
            {
                return NotFound();
            }

            ViewData.Add("toolbar", new[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontSize", "FontColor", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList", "|",
                "CreateLink", "Image", "|", "SourceCode"
            });

            var hasPolicyToEdit = await _authorization.AuthorizeAsync(User, Policies.CanManageForums);
            if (Post.Author.UserName == User.Identity.Name || hasPolicyToEdit.Succeeded)
            {
                return Page();
            }

            return LocalRedirect("/Identity/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var post = await _forumRepository.GetByIdAsync<Post>(Post.Id);

            if (post == null)
            {
                return NotFound();
            }

            post.Content = Post.Content;
            await _forumRepository.UpdateAsync(post);
            return RedirectToPage("./Index", new { slug = Post.Thread.Slug });
        }
    }
}
