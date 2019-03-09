using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Models.ForumModel;
using EC_WebSite.Data;

namespace EC_WebSite.Pages.Forums.Thread
{
    public class CreateThreadModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public CreateThreadModel(ApplicationDbContext db, UserManager<Models.UserModel.User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }     

        [BindProperty]
        public InputModel Input { get; set; }

        public Models.ForumModel.Board Board { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Topic name required")]
            public string Topic { get; set; }

            [Required(ErrorMessage = "Topic text required")]
            [DataType(DataType.MultilineText)]
            public string Text { get; set; }
        }        
        

        public IActionResult OnGet()
        {
            var boardId = RouteData.Values["boardId"].ToString();
            Board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var boardId = RouteData.Values["boardId"].ToString();
            var currentUser = await _userManager.GetUserAsync(User);
            var author = _db.Users.Where(i => i.Id == currentUser.Id).FirstOrDefault();
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

            var thread = new Models.ForumModel.Thread()
            {
                Author = author,
                Name = Input.Topic,
                Board = board
            };

            var post = new Post()
            {
                Author = author,
                Text = Input.Text,
                Thread = thread,
                CreatedTime = DateTime.Now
            };

            thread.Posts.Add(post);

            _db.Threads.Add(thread);            
            _db.SaveChanges();


            return RedirectToPage($"/Forums/Thread/Index", new { threadId = thread.Id });
        }
    }
}