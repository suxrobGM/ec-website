using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EC_WebSite.Pages.Home
{
    public class CreateArticleModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateArticleModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {            
            return Page();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Article Article { get; set; }
            public IFormFile CoverPhoto { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var file = HttpContext.Request.Form.Files.FirstOrDefault();
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (file != null)
            {
                byte[] imageBytes;
                using (var ms = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(ms);
                    imageBytes = ms.ToArray();
                    Input.Article.CoverPhoto = new Media() { Content = imageBytes, ContentType = file.ContentType };
                }
            }
            
            Input.Article.Author = _db.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            _db.Articles.Add(Input.Article);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}