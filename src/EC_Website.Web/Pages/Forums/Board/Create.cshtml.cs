using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Board
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class CreateModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public CreateModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty]
        public Core.Entities.ForumModel.Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(string forumId)
        {
            if (forumId == null)
            {
                return NotFound();
            }

            Board = new Core.Entities.ForumModel.Board()
            {
                Forum = await _forumRepository.GetByIdAsync<Forum>(forumId)
            };

            if (Board.Forum == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string forumId)
        {
            Board.Forum = await _forumRepository.GetByIdAsync<Forum>(forumId);
            Board.Slug = ArticleBase.CreateSlug(Board.Title);
            await _forumRepository.AddAsync(Board);
            return RedirectToPage("/Forums/Index");
        }
    }
}