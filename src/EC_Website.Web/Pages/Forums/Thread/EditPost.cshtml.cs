using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize(Policy = Policies.CanManageForums)]
    public class EditPostModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public EditPostModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
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
                "FontName", "FontSize", "FontColor", "BackgroundColor",
                "LowerCase", "UpperCase", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|",
                "CreateTable", "CreateLink", "Image", "|", "ClearFormat",
                "SourceCode", "FullScreen", "|", "Undo", "Redo"
            });

            return Page();
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
