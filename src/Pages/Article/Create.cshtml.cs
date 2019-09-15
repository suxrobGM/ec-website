using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using EC_WebSite.Data;
using EC_Website.Utils;

namespace EC_WebSite.Pages.Home
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
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Models.Blog.Article Article { get; set; }
            public IFormFile CoverPhoto { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {   
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.CoverPhoto != null)
            {
                var image = Input.CoverPhoto;
                var fileName = $"{Input.Article.Id}_article_cover.jpg";
                var fileNameAbsPath = Path.Combine(_env.WebRootPath, "db_files", "img", fileName);
                ImageHelper.ResizeToQuadratic(image.OpenReadStream(), fileNameAbsPath);
            }
            
            Input.Article.Author = _db.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            _db.Articles.Add(Input.Article);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}