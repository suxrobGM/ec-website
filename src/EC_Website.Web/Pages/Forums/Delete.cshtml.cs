using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums
{
    [Authorize(Roles = "SuperAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public DeleteModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty]
        public ForumHead Forum { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Forum = await _forumRepository.GetByIdAsync<ForumHead>(id);

            if (Forum == null)
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

            Forum = await _forumRepository.GetByIdAsync<ForumHead>(id);
            await _forumRepository.DeleteForumAsync(Forum);
            return RedirectToPage("/Index");
        }
    }
}
