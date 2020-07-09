using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Blog
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class EditModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _env;

        public EditModel(IBlogRepository blogRepository, IWebHostEnvironment env)
        {
            _blogRepository = blogRepository;
            _env = env;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Core.Entities.BlogModel.Blog Blog { get; set; }           
            public IFormFile CoverPhoto { get; set; }
            public string Tags { get; set; } 
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _blogRepository.GetByIdAsync<Core.Entities.BlogModel.Blog>(id);

            if (blog == null)
            {
                return NotFound();
            }

            Input = new InputModel
            {
                Blog = blog, 
                Tags = Tag.JoinTags(blog.BlogTags.Select(i => i.Tag))
            };

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

            var blog = await _blogRepository.GetByIdAsync<Core.Entities.BlogModel.Blog>(Input.Blog.Id);

            if (blog == null)
            {
                return NotFound();
            }

            var tags = Tag.ParseTags(Input.Tags);
            blog.Title = Input.Blog.Title;
            blog.Summary = Input.Blog.Summary;
            blog.Content = Input.Blog.Content;
            blog.Slug = Input.Blog.Title.Slugify();

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{blog.Id}_blog_cover.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                blog.CoverPhotoPath = $"/db_files/img/{fileName}";
            }

            await _blogRepository.AddTagsAsync(blog, false, tags);
            await _blogRepository.UpdateAsync(blog);
            return RedirectToPage("/Index");
        }
    }
}