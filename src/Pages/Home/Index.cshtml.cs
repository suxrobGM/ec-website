using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Data;

namespace EC_WebSite.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Models.Article> Articles { get; set; }

        public IActionResult OnGet()
        {
            Articles = _db.Articles;

            return Page();
        }
    }
}