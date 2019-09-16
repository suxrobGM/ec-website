using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Models.ForumModel;
using EC_Website.Data;

namespace EC_Website.Pages.Forums
{
    public class ThreadIndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public ThreadIndexModel(ApplicationDbContext db, UserManager<Models.UserModel.User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Models.ForumModel.Thread Thread { get; set; }
        public string SearchText { get; set; }
        public PaginatedList<Post> Posts { get; set; }

        [BindProperty]
        [DataType(DataType.MultilineText)]
        public string PostText { get; set; }
              
      
        public IActionResult OnGetAsync(int pageIndex = 1)
        {
            var threadId = RouteData.Values["threadId"].ToString();
            Thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();
            var posts = _db.Posts.Where(i => i.ThreadId == threadId);
            Posts = PaginatedList<Post>.Create(posts, pageIndex);            

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

        public async Task<IEnumerable<string>> GetUserRolesAsync(Models.UserModel.User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}