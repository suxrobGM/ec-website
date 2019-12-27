using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Models.UserModel;

namespace EC_Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Models.UserModel.User> _userManager;

        public IndexModel(UserManager<Models.UserModel.User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Home/Index");
        }

        private async Task AddRoleToUserAsync(Role role, string username)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);
            var userInRole = await _userManager.IsInRoleAsync(user, role.ToString()).ConfigureAwait(false);

            if (!userInRole)
            {
                await _userManager.AddToRoleAsync(user, role.ToString()).ConfigureAwait(false);
            }
        }
    }
}