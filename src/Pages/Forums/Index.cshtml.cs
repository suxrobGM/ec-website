using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Models.ForumModel;
using EC_Website.Data;

namespace EC_Website.Pages.Forums
{
    public class ForumsIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ForumsIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public string SearchText { get; set; }
        public IEnumerable<ForumHead> ForumHeads { get; set; }
        public IEnumerable<FavoriteThread> FavoriteThreads { get; set; }       

        public IActionResult OnGet()
        {           
            var userFavoriteThreads = _context.FavoriteThreads.Where(i => i.User.UserName == User.Identity.Name);
            ForumHeads = _context.ForumHeads;
            FavoriteThreads = userFavoriteThreads;

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteForumHeadAsync(string forumHeadId)
        {
            var forumHead = await _context.ForumHeads.Where(i => i.Id == forumHeadId).FirstAsync();

            foreach (var board in forumHead.Boards)
            {
                foreach (var posts in board.Threads.Select(i => i.Posts))
                {
                    _context.RemoveRange(posts);
                }

                _context.Remove(board);
            }

            _context.Remove(forumHead);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }

        public async Task<IActionResult> OnPostDeleteBoardAsync(string id)
        {
            var board = await _context.Boards.Where(i => i.Id == id).FirstAsync();

            foreach (var posts in board.Threads.Select(i => i.Posts))
            {
                _context.RemoveRange(posts);
            }

            _context.Remove(board);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string id)
        {
            var favoriteThread = await _context.FavoriteThreads.Where(i => i.ThreadId == id).FirstAsync();

            _context.FavoriteThreads.Remove(favoriteThread);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}