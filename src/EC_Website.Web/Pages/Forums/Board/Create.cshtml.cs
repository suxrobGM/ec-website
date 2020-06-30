using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Forum;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Forums.Board
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class CreateBoardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateBoardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ForumHead Forum { get; set; }

        [BindProperty]
        public Core.Entities.Forum.Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(string headId)
        {
            if (headId == null)
            {
                return NotFound();
            }

            Forum = await _context.ForumHeads.FirstOrDefaultAsync(i => i.Id == headId);

            if (Forum == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string headId)
        {
            Board.Forum = await _context.ForumHeads.FirstAsync(i => i.Id == headId);
            Board.Slug = ArticleBase.CreateSlug(Board.Title);
            await _context.Boards.AddAsync(Board);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}