using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;
using EC_Website.Models.Blog;

namespace EC_Website.Pages.Home
{
    public class HomeIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HomeIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<BlogEntry> BlogEntries { get; set; }

        public IActionResult OnGet(int pageIndex = 1)
        {
            BlogEntries = PaginatedList<BlogEntry>.Create(_context.BlogEntries, pageIndex);

            return Page();
        }
    }
}