using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize(Policy = Policies.CanManageForums)]
    public class EditModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public EditModel(IForumRepository forumRepository)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(Thread.Id);

            if (thread == null)
            {
                return NotFound();
            }

            thread.Title = Thread.Title;
            thread.Slug = Thread.Title.Slugify();
            thread.IsLocked = Thread.IsLocked;
            thread.IsPinned = Thread.IsPinned;
            await _forumRepository.UpdateThreadAsync(thread);
            return RedirectToPage("./Index", new {slug = thread.Slug});
        }
    }
}