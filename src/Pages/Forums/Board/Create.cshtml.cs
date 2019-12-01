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

        public string ForumTitle { get; set; }

        [BindProperty]
        public Models.ForumModel.Board Board { get; set; }

        public async Task<IActionResult> OnGetAsync(string forumHeadId)
        {
            if (forumHeadId == null)
            {
                return NotFound();
            }

            var forum = await _context.ForumHeads.Where(i => i.Id == forumHeadId).FirstOrDefaultAsync();

            if (forum == null)
            {
                return NotFound();
            }

            ForumTitle = forum.Title;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string forumHeadId)
        {
            Board.Forum = await _context.ForumHeads.Where(i => i.Id == forumHeadId).FirstAsync();
            Board.Slug = ArticleBase.CreateSlug(Board.Title);
            _context.Boards.Add(Board);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}