using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Blog
{
    [Authorize(Policy = Policies.CanManageBlogs)]
    public class CreateModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ImageHelper _imageHelper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IBlogRepository blogRepository, ImageHelper imageHelper,
            UserManager<ApplicationUser> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
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

        public IActionResult OnGet()
        {
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

            var tags = Tag.ParseTags(Input.Tags);
            Input.Blog.Author = await _userManager.GetUserAsync(User);
            Input.Blog.Slug = Input.Blog.Title.Slugify();

            if (Input.CoverPhoto != null)
            {
                Input.Blog.CoverPhotoPath = _imageHelper.UploadImage(Input.CoverPhoto, $"{Input.Blog.Id}_blog_cover", resizeToRectangle: true);                
            }

            await _blogRepository.UpdateTagsAsync(Input.Blog, false, tags);
            await _blogRepository.AddBlogAsync(Input.Blog);
            return RedirectToPage("./Index", new { slug = Input.Blog.Slug });
        }
    }
}