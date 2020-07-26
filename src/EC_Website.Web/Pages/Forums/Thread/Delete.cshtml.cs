using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize(Policy = Policies.CanManageForums)]
    public class DeleteModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public DeleteModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty]
        public Core.Entities.ForumModel.Thread Thread { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(id);

            if (Thread == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(id);
            var boardSlug = Thread.Board.Slug;
            await _forumRepository.DeleteThreadAsync(Thread);
            return RedirectToPage("/Forums/Board/Index", new { slug = boardSlug });
        }
    }
}
