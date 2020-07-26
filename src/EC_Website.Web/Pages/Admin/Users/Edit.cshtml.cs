using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Infrastructure.Extensions;
using EC_Website.Web.Authorization;
using EC_Website.Web.Utils;

namespace EC_Website.Web.Pages.Admin.Users
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class EditModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ImageHelper _imageHelper;

        public EditModel(UserManager<ApplicationUser> userManager,
            RoleManager<UserRole> roleManager, ImageHelper imageHelper,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _imageHelper = imageHelper;
        }

        [BindProperty]
        public ApplicationUser AppUser { get; set; }

        [BindProperty]
        public IList<string> UserRolesName { get; set; }

        [BindProperty]
        public IList<string> UserBadgesName { get; set; }

        [BindProperty]
        public IFormFile ProfilePhoto { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
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

            List<UserRole> userRoles;
            if (User.IsInRole("SuperAdmin"))
            {
                // Only SuperAdmin can assign Admin or SuperAdmin roles
                userRoles = await _roleManager.Roles.ToListAsync();
            }
            else
            {
                // Exclude SuperAdmin and Admin roles
                userRoles = await _roleManager.Roles.Where(i => i.Role != Role.SuperAdmin && i.Role != Role.Admin).ToListAsync();
            }
            
            var userBadges = await _userRepository.GetListAsync<Badge>();
            ViewData.Add("userRoles", userRoles);
            ViewData.Add("userBadges", userBadges.Select(i => i.Name));

            UserRolesName = await _userManager.GetRolesAsync(AppUser);
            UserBadgesName = AppUser.UserBadges.Select(i => i.Badge.Name).ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
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

            user.UserName = AppUser.UserName;
            user.Email = AppUser.Email;
            user.FirstName = AppUser.FirstName;
            user.LastName = AppUser.LastName;
            user.PhoneNumber = AppUser.PhoneNumber;
            user.Status = AppUser.Status;
            user.Bio = AppUser.Bio;
            user.IsBanned = AppUser.IsBanned;
            user.BanExpirationDate = AppUser.BanExpirationDate;
            await _userRepository.UpdateUserBadgesAsync(user, UserBadgesName);
            await _userRepository.UpdateUserRolesAsync(user, UserRolesName);

            if (ProfilePhoto != null)
            {
                user.ProfilePhotoPath = _imageHelper.UploadImage(ProfilePhoto, $"{user.Id}_profile", true);
            }

            await _userManager.UpdateAsync(user);
            return RedirectToPage("./Index");
        }
    }
}
