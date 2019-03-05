using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            public Models.Article Article { get; set; }
            public IFormFile CoverPhoto { get; set; }
        }

        public void OnGet(string articleId)
        {
            Input = new InputModel
            {
                Article = _db.Articles.Where(i => i.Id == articleId).FirstOrDefault()
            };
        }
    }
}