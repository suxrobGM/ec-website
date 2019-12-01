using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Models.ForumModel;
using EC_Website.Data;

namespace EC_Website.Pages.Forums
{
    public class BoardIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public BoardIndexModel(ApplicationDbContext context, UserManager<Models.UserModel.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public Models.ForumModel.Board Board { get; set; }
        public string SearchText { get; set; }          


        public async Task<IActionResult> OnGetAsync()
        {
            var boardSlug = RouteData.Values["slug"].ToString();
            Board = await _context.Boards.Where(i => i.Slug == boardSlug).FirstOrDefaultAsync();

            if (Board == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddFavoriteThreadAsync(string threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = await _context.Threads.Where(i => i.Id == threadId).FirstAsync();

            var favoriteThread = new FavoriteThread()
            {
                Thread = thread,
                User = currentUser
            };

            _context.FavoriteThreads.Add(favoriteThread);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}