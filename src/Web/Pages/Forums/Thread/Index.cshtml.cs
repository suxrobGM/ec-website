using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Models.ForumModel;
using EC_Website.Data;

namespace EC_Website.Pages.Forums
{
    public class ThreadIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public ThreadIndexModel(ApplicationDbContext context, UserManager<Models.UserModel.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Models.ForumModel.Thread Thread { get; set; }
        public string SearchText { get; set; }
        public PaginatedList<Post> Posts { get; set; }

        [BindProperty]
        public string PostContent { get; set; }
      
        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var threadSlug = RouteData.Values["slug"].ToString();
            Thread = await _context.Threads.FirstOrDefaultAsync(i => i.Slug == threadSlug);

            if (Thread == null)
            {
                return NotFound();
            }

            Posts = PaginatedList<Post>.Create(Thread.Posts, pageIndex);

            ViewData.Add("toolbar", new[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontName", "FontSize", "FontColor", "BackgroundColor",
                "LowerCase", "UpperCase", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|",
                "CreateTable", "CreateLink", "Image", "|", "ClearFormat",
                "SourceCode", "FullScreen", "|", "Undo", "Redo"
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var threadSlug = RouteData.Values["slug"].ToString();
            var thread = await _context.Threads.FirstAsync(i => i.Slug == threadSlug);
            var author = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);

            var post = new Post()
            {
                Author = author,
                Thread = thread,
                Content = PostContent,
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePostAsync(string postId)
        {
            var post = await _context.Posts.FirstAsync(i => i.Id == postId);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(Models.UserModel.User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}