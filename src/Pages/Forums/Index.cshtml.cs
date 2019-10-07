using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Models.ForumModel;
using EC_Website.Data;

namespace EC_Website.Pages.Forums
{
    public class ForumsIndexModel : PageModel
    {
        private readonly UserManager<Models.UserModel.User> _userManager;
        private readonly ApplicationDbContext _db;

        public ForumsIndexModel(ApplicationDbContext db, UserManager<Models.UserModel.User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
       
        public string SearchText { get; set; }
        public IEnumerable<ForumHead> ForumHeads { get; set; }
        public IEnumerable<FavoriteThread> FavoriteThreads { get; set; }       
        

        public async Task<IActionResult> OnGetAsync()
        {           
            var currentUser = await _userManager.GetUserAsync(User);
            var userFavoriteThreads = _db.FavoriteThreads.Where(i => i.UserId == currentUser.Id);
            ForumHeads = _db.ForumHeads;
            FavoriteThreads = userFavoriteThreads;

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteForumHeadAsync(string forumHeadId)
        {
            var forumHead = _db.ForumHeads.Where(i => i.Id == forumHeadId).First();

            foreach (var board in forumHead.Boards)
            {
                foreach (var posts in board.Threads.Select(i => i.Posts))
                {
                    _db.RemoveRange(posts);
                }

                _db.Remove(board);
            }

            _db.Remove(forumHead);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }

        public async Task<IActionResult> OnPostDeleteBoardAsync(string boardUrl)
        {
            var board = _db.Boards.Where(i => i.Url == boardUrl).First();

            foreach (var posts in board.Threads.Select(i => i.Posts))
            {
                _db.RemoveRange(posts);
            }

            _db.Remove(board);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string favoriteThreadId)
        {
            var favoriteThread = _db.FavoriteThreads.Where(i => i.ThreadId == favoriteThreadId).First();

            _db.FavoriteThreads.Remove(favoriteThread);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}