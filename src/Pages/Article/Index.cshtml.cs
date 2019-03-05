using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.Article
{
    public class ArticleIndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public ArticleIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.Article Article { get; set; }

        public void OnGet()
        {
            string articleId = RouteData.Values["articleId"].ToString();
            Article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
        }

        public async Task<IActionResult> OnPostDeleteArticleAsync(string articleId)
        {
            var article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
            _db.Articles.Remove(article);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}