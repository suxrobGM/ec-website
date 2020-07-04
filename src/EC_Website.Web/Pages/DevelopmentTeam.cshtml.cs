using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Web.Pages
{
    public class DevelopmentTeamModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DevelopmentTeamModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IList<ApplicationUser> Developers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Developers = await _userManager.GetUsersInRoleAsync(Role.Developer.ToString());
            return Page();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}