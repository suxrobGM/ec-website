using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Pages.Wiki
{
    public class IndexModel : PageModel
    {
        private readonly EC_Website.Data.ApplicationDbContext _context;

        public IndexModel(EC_Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<WikiArticle> WikiArticle { get;set; }

        public async Task OnGetAsync()
        {
            WikiArticle = await _context.WikiArticles
                .Include(w => w.Author).ToListAsync();
        }
    }
}
