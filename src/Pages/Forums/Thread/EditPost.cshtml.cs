using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.ForumModel;

namespace EC_Website.Pages.Forums.Thread
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class EditPostModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditPostModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Post Post { get; set; }

        public async Task<IActionResult> OnGetAsync(string postId)
        {
            if (postId == null)
            {
                return NotFound();
            }

            Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == postId);

            if (Post == null)
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

            var post = await _context.Posts.FirstAsync(i => i.Id == Post.Id);
            post.Content = Post.Content;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(Post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { slug = Post.Thread.Slug });
        }

        private bool PostExists(string id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
