using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Forums.Board
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
        public Core.Entities.ForumModel.Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Board = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Board>(id);

            if (Board == null)
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

            var board = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Board>(Board.Id);

            if (board == null)
            {
                return NotFound();
            }

            board.Title = Board.Title;
            board.Slug = Board.Title.Slugify();
            board.IsLocked = Board.IsLocked;
            await _forumRepository.UpdateBoardAsync(board);
            return RedirectToPage("./Index", new { slug = board.Slug });
        }
    }
}