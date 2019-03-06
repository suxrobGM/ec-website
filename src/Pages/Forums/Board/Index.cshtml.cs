using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Models;
using EC_WebSite.Models.ForumModel;

namespace EC_WebSite.Pages.Forums
{
    public class BoardIndexModel : PageModel
    {
        private ApplicationDbContext _db;
        private UserManager<Models.UserModel.User> _userManager;

        public BoardIndexModel(ApplicationDbContext db, UserManager<Models.UserModel.User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public Models.ForumModel.Board Board { get; set; }
        public string SearchText { get; set; }          


        public IActionResult OnGet()
        {
            var boardId = RouteData.Values["boardId"].ToString();
            Board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

            if (Board.Threads == null)
                Board.Threads = new List<Models.ForumModel.Thread>();                       

            return Page();
        }

        public async Task<IActionResult> OnPostAddFavoriteThreadAsync(string threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();

            var favoriteThread = new FavoriteThread()
            {
                Thread = thread,
                User = currentUser
            };

            try
            {
                _db.FavoriteThreads.Add(favoriteThread);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToPage();
            }

            return RedirectToPage();
        }
    }
}