using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;
using EC_Website.Models.Blog;

namespace EC_Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<BlogEntry> BlogEntries { get; set; }

        public IActionResult OnGet(int pageIndex = 1)
        {
            BlogEntries = PaginatedList<BlogEntry>.Create(_context.BlogEntries, pageIndex, 5);

            return Page();
        }
    }
}