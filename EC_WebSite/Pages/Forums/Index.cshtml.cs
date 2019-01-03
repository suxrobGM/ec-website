using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.Forums
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }       
        public IEnumerable<ForumHead> ForumHeads { get; set; }
        public IEnumerable<FavoriteThread> FavoriteThreads { get; set; }

        public class InputModel
        {            
            public string SearchText { get; set; }
            public string SelectedFavoriteThreadId { get; set; }
            public string SelectedForumHeadId { get; set; }
            public string SelectedBoardId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new InputModel();
            var currentUser = await _userManager.GetUserAsync(User);
            var userFavoriteThreads = _db.FavoriteThreads.Where(i => i.UserId == currentUser.Id);
            ForumHeads = _db.ForumHeads;
            FavoriteThreads = userFavoriteThreads;
            
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteForumHeadAsync()
        {
            await Task.Run(() =>
            {
                var forumHead = _db.ForumHeads.Where(i => i.Id == Input.SelectedForumHeadId).FirstOrDefault();

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
    }
}