using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class DeleteModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public DeleteModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty]
        public Core.Entities.Forum.Thread Thread { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Thread = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Thread>(id);

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

            Thread = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Thread>(id);
            await _forumRepository.DeleteThreadAsync(Thread);
            return RedirectToPage("/Board/Index", new { slug = Thread.Board.Slug });
        }
    }
}
