using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Forums.Thread
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IForumRepository forumRepository,
            UserManager<ApplicationUser> userManager)
        {
            _forumRepository = forumRepository;
            _userManager = userManager;
        }     

        [BindProperty]
        public Core.Entities.Forum.Thread Thread { get; set; }

        [BindProperty]
        public Post Post { get; set; }


        public async Task<IActionResult> OnGetAsync(string boardId)
        {
            if (boardId == null)
            {
                return NotFound();
            }

            Thread = new Core.Entities.Forum.Thread()
            {
                Board = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Board>(boardId)
            };

            if (Thread.Board == null)
            {
                return NotFound();
            }

            ViewData.Add("toolbar", new[]
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

        public async Task<IActionResult> OnPostAsync(string boardId)
        {
            var author = await _userManager.GetUserAsync(User);
            var board = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Board>(boardId);

            if (board == null)
            {
                return NotFound();
            }

            Thread.Author = author;
            Thread.Slug = ArticleBase.CreateSlug(Thread.Title);
            Thread.Board = board;
            Thread.Posts.Add(new Post()
            {
                Content = Post.Content,
                Author = author
            });

            await _forumRepository.AddAsync(Thread);
            return RedirectToPage("./Index", new { slug = Thread.Slug });
        }
    }
}