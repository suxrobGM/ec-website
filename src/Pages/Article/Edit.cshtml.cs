using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Data;

namespace EC_WebSite.Pages.Article
{
    public class EditArticleModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditArticleModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Models.Blog.Article Article { get; set; }           
            public IFormFile CoverPhoto { get; set; }
        }

        public void OnGet(string articleId)
        {           
            Input = new InputModel
            {
                Article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault()
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string articleId = RouteData.Values["articleId"].ToString();
            var article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
           
            article.Topic = Input.Article.Topic;
            article.Summary = Input.Article.Summary;
            article.Text = Input.Article.Text;

            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}