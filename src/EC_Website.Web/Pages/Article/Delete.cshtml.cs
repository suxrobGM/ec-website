using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.Blog;
using EC_Website.Infrastructure.Data;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Article
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class DeleteArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteArticleModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public BlogEntry Entry { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entry = await _context.BlogEntries
                .Include(b => b.Author).FirstOrDefaultAsync(m => m.Id == id);


            if (Entry == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entry = await _context.BlogEntries.FindAsync(id);

            if (Entry != null)
            {
                _context.BlogEntries.Remove(Entry);
                ImageHelper.RemoveImage(Entry.CoverPhotoPath, _env);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
