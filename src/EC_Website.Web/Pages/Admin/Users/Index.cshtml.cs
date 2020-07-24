using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Admin.Users
{
    [Authorize(Policy = Policies.HasAdminRole)]
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
            var users = _userManager.Users;
            TotalUsers = await users.CountAsync();
            Users = PaginatedList<ApplicationUser>.Create(users, pageIndex, 100);
            return Page();
        }
    }
}
