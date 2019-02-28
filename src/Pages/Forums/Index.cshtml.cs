using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Models;
using EC_WebSite.Models.ForumModel;
using EC_WebSite.Models.UserModel;

namespace EC_WebSite.Pages.Forums
{
    public class ForumsIndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public ForumsIndexModel(ApplicationDbContext db, UserManager<User> userManager)
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
            await Task.Run(() =>
            {
                var forumHead = _db.ForumHeads.Where(i => i.Id == forumHeadId).FirstOrDefault();

                foreach (var board in forumHead.Boards)
                {
                    foreach (var posts in board.Threads.Select(i => i.Posts))
                    {
                        _db.RemoveRange(posts);
                    }

                    _db.Remove(board);
                }

                _db.Remove(forumHead);
                _db.SaveChanges();
            });

            return RedirectToPage("/Forums/Index");
        }

        public async Task<IActionResult> OnPostDeleteBoardAsync(string boardId)
        {
            await Task.Run(() =>
            {
                var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

                foreach (var posts in board.Threads.Select(i => i.Posts))
                {
                    _db.RemoveRange(posts);
                }

                _db.Remove(board);
                _db.SaveChanges();
            });

            return RedirectToPage("/Forums/Index");
        }

        public IActionResult OnPostRemoveFromFavoriteThreads(string favoriteThreadId)
        {
            var favoriteThread = _db.FavoriteThreads.Where(i => i.ThreadId == favoriteThreadId).FirstOrDefault();

            _db.FavoriteThreads.Remove(favoriteThread);
            _db.SaveChanges();

            return RedirectToPage("/Forums/Index");
        }
    }
}