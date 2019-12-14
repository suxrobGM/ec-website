using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Utils;
using EC_Website.Models;

namespace EC_Website.Pages.Article
{
    public class EditArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EditArticleModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Models.Blog.BlogArticle Article { get; set; }           
            public IFormFile CoverPhoto { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.BlogArticles.FirstAsync(i => i.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            Input = new InputModel() { Article = article };         

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

            var article = await _context.BlogArticles.FirstAsync(i => i.Id == Input.Article.Id);       
            article.Title = Input.Article.Title;
            article.Summary = Input.Article.Summary;
            article.Content = Input.Article.Content;
            article.Tags = Input.Article.Tags;
            article.Slug = ArticleBase.CreateSlug(Input.Article.Title);

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{article.Id}_cover.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                article.CoverPhotoUrl = $"/db_files/img/{fileName}";
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}