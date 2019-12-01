using System.Linq;
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

            Forum = await _context.ForumHeads.Where(i => i.Id == headId).FirstOrDefaultAsync();

            if (Forum == null)
            {
                return NotFound();
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string headId)
        {
            Board.Forum = await _context.ForumHeads.Where(i => i.Id == headId).FirstAsync();
            Board.Slug = ArticleBase.CreateSlug(Board.Title);
            _context.Boards.Add(Board);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}