using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;

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

            //var tools = new
            //{
            //    tooltipText = "Insert Emoticons",
            //    template = "<button class='e-tbar-btn e-btn' tabindex='-1' id='emot_tbar'  style='width:100%'><div class='e-tbar-btn-text rtecustomtool' style='font-weight: 500;'> &#128578;</div></button>"
            //};

            ViewData.Add("toolbar", new object[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontSize", "FontColor", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList", "|",
                "CreateLink", "Image", "|", "SourceCode"
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAddPostAsync()
        {
            if (string.IsNullOrEmpty(PostContent))
            {
                ModelState.AddModelError("PostContent", "Required Post Content");
            }

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
    }
}