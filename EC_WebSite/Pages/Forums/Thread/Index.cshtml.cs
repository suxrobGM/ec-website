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
    public class ThreadIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public ThreadIndexModel(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Models.Thread Thread { get; set; }
        public string SearchText { get; set; }

        [BindProperty]
        [DataType(DataType.MultilineText)]
        public string PostText { get; set; }
              

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public IActionResult OnGet()
        {
            var threadId = RouteData.Values["threadId"].ToString();
            Thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();

            return Page();
        }

        public async Task<IActionResult> OnPostAddPost()
        {
            var threadId = RouteData.Values["threadId"].ToString();
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();
            var author = _db.Users.Where(i => i.Id == currentUser.Id).FirstOrDefault();

            var post = new Post()
            {
                Author = author,
                Thread = thread,
                Text = PostText,
            };

            _db.Posts.Add(post);
            await _db.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePostAsync(string postId)
        {
            var post = _db.Posts.Where(i => i.Id == postId).FirstOrDefault();
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}