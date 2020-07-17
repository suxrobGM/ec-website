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
    public class DeletePostModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public DeletePostModel(IForumRepository forumRepository)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string postId, string threadId)
        {
            if (postId == null || threadId == null)
            {
                return NotFound();
            }

            Post = await _forumRepository.GetByIdAsync<Post>(postId);
            var thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(threadId);
            await _forumRepository.DeletePostAsync(Post);
            return RedirectToPage("./Index", new { slug = thread.Slug });
        }
    }
}
