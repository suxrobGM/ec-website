using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Infrastructure.Extensions;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Admin.Users
{
    [Authorize(Policy = Policies.CanBanUsers)]
    public class RestrictModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RestrictModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser AppUser { get; set; }

        [BindProperty]
        public string ReturnUrl { get; set; }
        
        public async Task<IActionResult> OnGetAsync(string id, string returnUrl = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser = await _userManager.FindByIdAsync(id);

            if (AppUser == null)
            {
                return NotFound();
            }

            var isUserRoleLower = await _userManager.CheckRoleLowerOrEqualAsync(User, AppUser);
            if (isUserRoleLower)
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }

            ReturnUrl = returnUrl ?? Url.Content($"~/User/Index/{AppUser.UserName}");
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(AppUser.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsBanned = AppUser.IsBanned;
            user.BanExpirationDate = AppUser.BanExpirationDate;
            await _userManager.UpdateAsync(user);
            return LocalRedirect(ReturnUrl);
        }
    }
}