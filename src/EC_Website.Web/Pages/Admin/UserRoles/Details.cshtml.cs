using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.User;

namespace EC_Website.Web.Pages.Admin.UserRoles
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly RoleManager<UserRole> _roleManager;

        public DetailsModel(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public UserRole UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = await _roleManager.FindByIdAsync(id);

            if (UserRole == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
