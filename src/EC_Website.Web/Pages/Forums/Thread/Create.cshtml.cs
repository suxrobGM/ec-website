using System.ComponentModel.DataAnnotations;
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
    public class CreateThreadModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateThreadModel(IForumRepository forumRepository,
            UserManager<ApplicationUser> userManager)
        {
            _forumRepository = forumRepository;
            _userManager = userManager;
        }     

        [BindProperty]
        public InputModel Input { get; set; }

        public Core.Entities.Forum.Board Board { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Topic name required")]
            public string Title { get; set; }

            [Required(ErrorMessage = "Topic text required")]
            [DataType(DataType.MultilineText)]
            public string PostContent { get; set; }
        }        
        

        public async Task<IActionResult> OnGetAsync(string boardId)
        {
            if (boardId == null)
            {
                return NotFound();
            }

            Board = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Board>(boardId);

            if (Board == null)
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

            var thread = new Core.Entities.Forum.Thread()
            {
                Title = Input.Title,
                Author = author,
                Board = board
            };

            var post = new Post()
            {
                Content = Input.PostContent,
                Author = author,
                Thread = thread
            };

            thread.Posts.Add(post);
            thread.Slug = ArticleBase.CreateSlug(thread.Title);
            await _forumRepository.AddAsync(thread);
            return RedirectToPage("./Index", new { slug = thread.Slug });
        }
    }
}