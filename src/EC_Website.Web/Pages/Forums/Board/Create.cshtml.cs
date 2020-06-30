using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Board
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class CreateBoardModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public CreateBoardModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        public ForumHead Forum { get; set; }

        [BindProperty]
        public Core.Entities.Forum.Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(string forumId)
        {
            if (forumId == null)
            {
                return NotFound();
            }

            Forum = await _forumRepository.GetByIdAsync<ForumHead>(forumId);

            if (Forum == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string forumId)
        {
            Board.Forum = await _forumRepository.GetByIdAsync<ForumHead>(forumId);
            Board.Slug = ArticleBase.CreateSlug(Board.Title);
            await _forumRepository.AddAsync(Board);
            return RedirectToPage("/Forums/Index");
        }
    }
}