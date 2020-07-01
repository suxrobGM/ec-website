using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Blog;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Article
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly IWebHostEnvironment _env;

        public EditModel(IRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public BlogEntry Entry { get; set; }           
            public IFormFile CoverPhoto { get; set; }
            public string ArticleTags { get; set; } 
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _repository.GetByIdAsync<BlogEntry>(id);

            if (article == null)
            {
                return NotFound();
            }

            Input = new InputModel { Entry = article, ArticleTags = string.Join(',', article.Tags) };

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

            var article = await _repository.GetByIdAsync<BlogEntry>(Input.Entry.Id);

            if (article == null)
            {
                return NotFound();
            }

            article.Title = Input.Entry.Title;
            article.Summary = Input.Entry.Summary;
            article.Content = Input.Entry.Content;
            article.Tags = Input.ArticleTags.Split(',', StringSplitOptions.RemoveEmptyEntries);
            article.Slug = ArticleBase.CreateSlug(Input.Entry.Title);

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{article.Id}_article.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                article.CoverPhotoPath = $"/db_files/img/{fileName}";
            }

            await _repository.UpdateAsync(article);
            return RedirectToPage("/Index");
        }
    }
}