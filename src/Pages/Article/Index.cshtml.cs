using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Data;
using EC_WebSite.Models.Blog;
using Microsoft.AspNetCore.Identity;

namespace EC_WebSite.Pages.Article
{
    public class ArticleIndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public ArticleIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.Blog.Article Article { get; set; }

        [BindProperty]
        public string CommentText { get; set; }

        public void OnGet()
        {
            string articleId = RouteData.Values["articleId"].ToString();
            Article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
        }

        public async Task<IActionResult> OnPostAddCommentAsync()
        {
            string articleId = RouteData.Values["articleId"].ToString();
            string userName = User.Identity.Name;
            var article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
            var author = _db.Users.Where(i => i.UserName == userName).FirstOrDefault();
            article.Comments.Add(new Comment()
            {
                Author = author,
                Text = CommentText,
            });
            await _db.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostReplyToCommentAsync(string commentId)
        {
            return RedirectToPage();
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