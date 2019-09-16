using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using EC_Website.Data;
using EC_Website.Utils;

namespace EC_Website.Pages.Article
{
    public class EditArticleModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _env;

        public EditArticleModel(ApplicationDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Models.Blog.Article Article { get; set; }           
            public IFormFile CoverPhoto { get; set; }
        }

        public void OnGet(string id)
        {           
            Input = new InputModel
            {
                Article = _db.Articles.Where(i => i.Id == id).FirstOrDefault()
            };

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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string articleUrl = RouteData.Values["articleUrl"].ToString();
            var article = _db.Articles.Where(i => i.Url == articleUrl).FirstOrDefault();
           
            article.Title = Input.Article.Title;
            article.Summary = Input.Article.Summary;
            article.Content = Input.Article.Content;
            article.Tags = Input.Article.Tags;

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{article.Id}_cover.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                article.CoverPhotoUrl = $"/db_files/img/{fileName}";
            }

            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}