using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models;

namespace EC_Website.Pages.Forums.Board
{
    public class CreateBoardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateBoardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.ForumModel.ForumHead Forum { get; set; }

        [BindProperty]
        public Models.ForumModel.Board Board { get; set; }

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
            _context.Boards.Add(Board);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}