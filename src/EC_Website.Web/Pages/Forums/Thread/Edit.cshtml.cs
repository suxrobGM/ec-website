using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class EditModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public EditModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty]
        public Core.Entities.Forum.Thread Thread { get; set; }

        [BindProperty]
        public string PostContent { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var thread = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Thread>(Thread.Id);

            if (thread == null)
            {
                return NotFound();
            }

            thread.Title = Thread.Title;
            thread.Slug = ArticleBase.CreateSlug(thread.Title);
            thread.IsLocked = Thread.IsLocked;
            thread.IsPinned = Thread.IsPinned;
            await _forumRepository.UpdateAsync(thread);
            return RedirectToPage("/Index");
        }
    }
}