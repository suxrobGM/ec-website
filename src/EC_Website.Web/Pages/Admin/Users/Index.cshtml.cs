using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Core.Entities.User;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Admin.Users
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalUsers { get; set; }
        public PaginatedList<ApplicationUser> Users { get;set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var users = _context.Users.AsNoTracking();
            TotalUsers = await users.CountAsync();
            Users = await PaginatedList<ApplicationUser>.CreateAsync(users, pageIndex, 100);

            return Page();
        }
    }
}
