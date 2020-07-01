using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums
{
    [Authorize(Roles = "SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public EditModel(IForumRepository forumRepository)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var forum = await _forumRepository.GetByIdAsync<ForumHead>(Forum.Id);

            if (forum == null)
            {
                return NotFound();
            }

            forum.Title = Forum.Title;
            await _forumRepository.UpdateAsync(forum);
            return RedirectToPage("./Index");
        }
    }
}