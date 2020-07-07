using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Web.ViewComponents
{
    public class UserRolesViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRolesViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return View(roles);
        }
    }
}
