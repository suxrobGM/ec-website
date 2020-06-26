using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Blog;
using EC_Website.Infrastructure.Data;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Article
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class CreateArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateArticleModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public BlogEntry Entry { get; set; }
            public IFormFile CoverPhoto { get; set; }
        }

        public IActionResult OnGet()
        {
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

            Input.Entry.Author = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);
            Input.Entry.Slug = ArticleBase.CreateSlug(Input.Entry.Title);

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{Input.Entry.Id}_article.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                Input.Entry.CoverPhotoPath = $"/db_files/img/{fileName}";                
            }
            
            await _context.BlogEntries.AddAsync(Input.Entry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { slug = Input.Entry.Slug });
        }
    }
}