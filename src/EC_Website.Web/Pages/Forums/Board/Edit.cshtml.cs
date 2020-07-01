using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Board
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
        public Core.Entities.Forum.Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Board = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Board>(id);

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

            var board = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Board>(Board.Id);

            if (board == null)
            {
                return NotFound();
            }

            board.Title = Board.Title;
            board.Slug = ArticleBase.CreateSlug(board.Title);
            await _forumRepository.UpdateAsync(board);
            return RedirectToPage("./Index", new { slug = board.Slug });
        }
    }
}