using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;

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
        public Core.Entities.ForumModel.Thread Thread { get; set; }

        [BindProperty]
        public Post Post { get; set; }


        public async Task<IActionResult> OnGetAsync(string boardId)
        {
            if (boardId == null)
            {
                return NotFound();
            }

            Thread = new Core.Entities.ForumModel.Thread()
            {
                Board = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Board>(boardId)
            };

            if (Thread.Board == null)
            {
                return NotFound();
            }

            ViewData.Add("toolbar", new[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontSize", "FontColor", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList", "|",
                "CreateLink", "Image", "|", "SourceCode"
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string boardId)
        {
            var author = await _userManager.GetUserAsync(User);
            var board = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Board>(boardId);

            if (board == null)
            {
                return NotFound();
            }

            Thread.Author = author;
            Thread.Slug = Thread.Title.Slugify();
            Thread.Board = board;
            Thread.Posts.Add(new Post()
            {
                Content = Post.Content,
                Author = author
            });

            await _forumRepository.AddThreadAsync(Thread);
            return RedirectToPage("./Index", new { slug = Thread.Slug });
        }
    }
}