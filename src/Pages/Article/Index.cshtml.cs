using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_WebSite.Data;
using EC_WebSite.Models.Blog;
using EC_WebSite.Utils;

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
        public PaginatedList<Comment> Comments { get; set; }

        [BindProperty]
        public string CommentText { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            string articleId = RouteData.Values["articleId"].ToString();

            Article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();            
            var comments = _db.Comments.Where(i => i.BlogId == articleId);
            Comments = await PaginatedList<Comment>.CreateAsync(comments, pageIndex, 2);

            return Page();
        }

        public async Task<IActionResult> OnPostAddCommentAsync()
        {
            string articleId = RouteData.Values["articleId"].ToString();
            string userName = User.Identity.Name;

            var article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
            var author = _db.Users.Where(i => i.UserName == userName).FirstOrDefault();
            var comment = new Comment()
            {
                Author = author,
                Text = CommentText,
            };
            article.Comments.Add(comment);

            await _db.SaveChangesAsync();
            return RedirectToPage("", "", $"{comment.Id}");
        }

        public async Task<IActionResult> OnPostReplyToCommentAsync(string commentId)
        {
            string userName = User.Identity.Name;

            var comment = _db.Comments.Where(i => i.Id == commentId).FirstOrDefault();
            var author = _db.Users.Where(i => i.UserName == userName).FirstOrDefault();
            var commentReply = new Comment()
            {
                Author = author,
                Blog = comment.Blog,
                Text = CommentText
            };
            comment.Replies.Add(commentReply);

            await _db.SaveChangesAsync();
            return RedirectToPage("", "", $"{commentReply.Id}");
        }

        public async Task<IActionResult> OnPostDeleteArticleAsync(string articleId)
        {
            var article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault();
            _db.Articles.Remove(article);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostDeleteCommentAsync(string commentId)
        {
            var comment = _db.Comments.Where(i => i.Id == commentId).FirstOrDefault();
            _db.Comments.Remove(comment);

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }      
    }
}