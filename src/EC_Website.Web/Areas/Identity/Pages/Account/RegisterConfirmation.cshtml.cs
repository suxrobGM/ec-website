using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterConfirmationModel(UserManager<ApplicationUser> userManager, 
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                null,
                new { area = "Identity", userId, code, returnUrl },
                Request.Scheme);

            await _emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(EmailConfirmationUrl)}'>clicking here</a>.");

            return Page();
        }
    }
}
