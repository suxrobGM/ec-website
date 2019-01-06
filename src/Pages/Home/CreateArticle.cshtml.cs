using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EC_WebSite.Models;

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
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Article.Author = _db.Users.Where(i => i.UserName == User.Identity.Name).FirstOrDefault();

            _db.Articles.Add(Article);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}