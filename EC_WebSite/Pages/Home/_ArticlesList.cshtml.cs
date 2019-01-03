using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_WebSite.Models;

namespace EC_WebSite.Pages.Home
{
    public class _ArticlesListModel : PageModel
    {
        private readonly EC_WebSite.Models.ApplicationDbContext _context;

        public _ArticlesListModel(EC_WebSite.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles
                .Include(a => a.Author).ToListAsync();
        }
    }
}
