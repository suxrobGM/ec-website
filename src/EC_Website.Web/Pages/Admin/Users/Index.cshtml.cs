using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Web.Pages.Admin.Users
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public int TotalUsers { get; set; }
        public PaginatedList<ApplicationUser> Users { get;set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var users = _userManager.Users.AsNoTracking();
            TotalUsers = await users.CountAsync();
            Users = await PaginatedList<ApplicationUser>.CreateAsync(users, pageIndex, 100);

            return Page();
        }
    }
}
