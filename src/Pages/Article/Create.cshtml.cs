using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using EC_Website.Data;
using EC_Website.Utils;

namespace EC_Website.Pages.Article
{
    public class CreateArticleModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _env;

        public CreateArticleModel(ApplicationDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult OnGet()
        {
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

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public EC_Website.Models.Blog.Article Article { get; set; }
            public IFormFile CoverPhoto { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_db.Articles.Where(i => i.Url == Input.Article.Url).Any())
            {
                ModelState.AddModelError("Article.Url", "This article url exists please change it to another url");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{Input.Article.Id}_article_cover.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                Input.Article.CoverPhotoUrl = $"/db_files/img/{fileName}";
            }

            Input.Article.Url = "/Article/" + Input.Article.Url.Trim().Replace(" ", "-");
            Input.Article.Author = _db.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();
            _db.Articles.Add(Input.Article);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}