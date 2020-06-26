using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Entities.User;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Forums
{
    public class BoardIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BoardIndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public Core.Entities.Forum.Board Board { get; set; }
        public string SearchText { get; set; }          


        public async Task<IActionResult> OnGetAsync()
        {
            var boardSlug = RouteData.Values["slug"].ToString();
            Board = await _context.Boards.FirstOrDefaultAsync(i => i.Slug == boardSlug);

            if (Board == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToFavoriteThreadsAsync(string threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = await _context.Threads.FirstAsync(i => i.Id == threadId);

            var favoriteThread = new FavoriteThread()
            {
                Thread = thread,
                User = currentUser
            };

            await _context.FavoriteThreads.AddAsync(favoriteThread);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string threadId)
        {
            var favoriteThread = await _context.FavoriteThreads.FirstAsync(i => i.ThreadId == threadId);

            _context.FavoriteThreads.Remove(favoriteThread);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}