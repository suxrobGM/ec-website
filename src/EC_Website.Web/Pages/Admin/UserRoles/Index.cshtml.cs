using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Admin.UserRoles
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class IndexModel : PageModel
    {
        private readonly RoleManager<UserRole> _roleManager;

        public IndexModel(RoleManager<UserRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            UserManager = userManager;
        }

        public IList<UserRole> UserRoles { get;set; }
        public UserManager<ApplicationUser> UserManager { get; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserRoles = await _roleManager.Roles.ToListAsync();

            return Page();
        }
    }
}
