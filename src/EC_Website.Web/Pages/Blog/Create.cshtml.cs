using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Blog
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class CreateModel : PageModel
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IBlogRepository blogRepository, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
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

            var tags = Input.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in tags)
            {
                Input.Blog.BlogTags.Add(new BlogTag()
                {
                    Tag = new Tag { Name = tag }
                });
            }

            Input.Blog.Author = await _userManager.GetUserAsync(User);
            Input.Blog.Slug = Input.Blog.Title.Slugify();
            

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{Input.Blog.Id}_article.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToRectangle(image.OpenReadStream(), fileNameAbsPath);
                Input.Blog.CoverPhotoPath = $"/db_files/img/{fileName}";                
            }
            
            await _blogRepository.AddAsync(Input.Blog);
            return RedirectToPage("./Index", new { slug = Input.Blog.Slug });
        }
    }
}