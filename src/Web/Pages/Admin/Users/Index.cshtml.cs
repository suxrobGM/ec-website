using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Data;

namespace EC_Website.Pages.Admin.Users
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
        public PaginatedList<Models.UserModel.User> Users { get;set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var users = _context.Users.AsNoTracking();
            TotalUsers = await users.CountAsync();
            Users = await PaginatedList<Models.UserModel.User>.CreateAsync(users, pageIndex, 100);

            return Page();
        }
    }
}
