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
    public class EditModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public EditModel(IForumRepository forumRepository)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var forum = await _forumRepository.GetByIdAsync<Forum>(Forum.Id);

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