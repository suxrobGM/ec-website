using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Blog
{
    [Authorize(Policy = Policies.CanManageBlogs)]
    public class EditModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ImageHelper _imageHelper;

        public EditModel(IBlogRepository blogRepository, ImageHelper imageHelper)
        {
            _blogRepository = blogRepository;
            _imageHelper = imageHelper;
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
                "FontName", "FontSize", "FontColor", "BackgroundColor", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|", "CreateTable", "CreateLink", "Image", "|", 
                "ClearFormat", "SourceCode", "FullScreen", "|", "Undo", "Redo"
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
                blog.CoverPhotoPath = _imageHelper.UploadImage(Input.CoverPhoto, $"{blog.Id}_blog_cover", resizeToRectangle: true);
            }
            else
            {
                _imageHelper.RemoveImage(blog.CoverPhotoPath);
                blog.CoverPhotoPath = "/img/ec_background.jpg";
            }

            await _blogRepository.UpdateTagsAsync(blog, false, tags);
            await _blogRepository.UpdateBlogAsync(blog);
            return RedirectToPage("/Index");
        }
    }
}