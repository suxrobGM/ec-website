using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Forums
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class DeleteModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public DeleteModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty]
        public Forum Forum { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Forum = await _forumRepository.GetByIdAsync<Forum>(id);

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

            Forum = await _forumRepository.GetByIdAsync<Forum>(id);
            await _forumRepository.DeleteForumAsync(Forum);
            return RedirectToPage("./Index");
        }
    }
}
