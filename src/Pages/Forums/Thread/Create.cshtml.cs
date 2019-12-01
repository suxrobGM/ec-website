using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Models.ForumModel;
using EC_Website.Data;
using EC_Website.Models;

namespace EC_Website.Pages.Forums.Thread
{
    public class CreateThreadModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateThreadModel(ApplicationDbContext context)
        {
            _context = context;
        }     

        [BindProperty]
        public InputModel Input { get; set; }

        public Models.ForumModel.Board Board { get; set; }

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

            Board = await _context.Boards.Where(i => i.Id == boardId).FirstOrDefaultAsync();

            if (Board == null)
            {
                return NotFound();
            }

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

        public async Task<IActionResult> OnPostAsync(string boardId)
        {
            var author = await _context.Users.Where(i => i.UserName == User.Identity.Name).FirstAsync();
            var board = await _context.Boards.Where(i => i.Id == boardId).FirstAsync();

            var thread = new Models.ForumModel.Thread()
            {
                Author = author,
                Title = Input.Title,
                Board = board
            };

            var post = new Post()
            {
                Author = author,
                Content = Input.PostContent,
                Thread = thread,
                Timestamp = DateTime.Now
            };

            thread.Posts.Add(post);
            thread.Slug = ArticleBase.CreateSlug(thread.Title);
            _context.Threads.Add(thread);            
            await _context.SaveChangesAsync();

            return RedirectToPage($"./Index", new { slug = thread.Slug });
        }
    }
}