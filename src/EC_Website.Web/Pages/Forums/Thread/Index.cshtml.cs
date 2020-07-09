using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Thread
{
    public class IndexModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IForumRepository forumRepository, UserManager<ApplicationUser> userManager)
        {
            _forumRepository = forumRepository;
            _userManager = userManager;
        }

        public Core.Entities.ForumModel.Thread Thread { get; set; }
        public PaginatedList<Post> Posts { get; set; }

        [BindProperty]
        public string PostContent { get; set; }
      
        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var threadSlug = RouteData.Values["slug"].ToString();
            Thread = await _forumRepository.GetAsync<Core.Entities.ForumModel.Thread>(i => i.Slug == threadSlug);

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

        public async Task<IActionResult> OnPostAddPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var threadSlug = RouteData.Values["slug"].ToString();
            var thread = await _forumRepository.GetAsync<Core.Entities.ForumModel.Thread>(i => i.Slug == threadSlug);
            var author = await _userManager.GetUserAsync(User);

            var post = new Post()
            {
                Author = author,
                Thread = thread,
                Content = PostContent,
            };

            await _forumRepository.AddAsync(post);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeletePostAsync(string postId)
        {
            var post = await _forumRepository.GetByIdAsync<Post>(postId);
            await _forumRepository.DeletePostAsync(post);
            return RedirectToPage();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}