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
              
      
        public IActionResult OnGet(int pageIndex = 1)
        {
            var threadUrl = RouteData.Values["threadUrl"].ToString();
            Thread = _db.Threads.Where(i => i.Url == threadUrl).First();
            Posts = PaginatedList<Post>.Create(Thread.Posts, pageIndex);

            ViewData.Add("toolbars", new string[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontName", "FontSize", "FontColor", "BackgroundColor",
                "LowerCase", "UpperCase", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|",
                "CreateTable", "CreateLink", "Image", "|", "ClearFormat", "Print",
                "SourceCode", "FullScreen", "|", "Undo", "Redo"
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAddPostAsync()
        {
            var threadUrl = RouteData.Values["threadUrl"].ToString();
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = _db.Threads.Where(i => i.Url == threadUrl).First();
            var author = _db.Users.Where(i => i.Id == currentUser.Id).First();

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
            var post = _db.Posts.Where(i => i.Id == postId).First();
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