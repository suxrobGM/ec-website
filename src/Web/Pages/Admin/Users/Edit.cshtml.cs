using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.UserModel;

namespace EC_Website.Pages.Admin.Users
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<Models.UserModel.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Models.UserModel.User AppUser { get; set; }

        [BindProperty]
        public IList<string> UserRolesName { get; set; }

        [BindProperty]
        public IList<string> UserBadgesName { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (AppUser == null)
            {
                return NotFound();
            }

            var userRoles = await _context.Roles.Where(i => i.Role != Role.SuperAdmin).Select(i => i.Name).ToArrayAsync();
            var userBadges = await _context.Badges.Select(i => i.Name).ToArrayAsync();
            ViewData.Add("userRoles", userRoles);
            ViewData.Add("userBadges", userBadges);

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

            var user = await _context.Users.FirstAsync(i => i.Id == AppUser.Id);
            user.UserName = AppUser.UserName;
            user.Email = AppUser.Email;
            user.FirstName = AppUser.FirstName;
            user.LastName = AppUser.LastName;
            user.PhoneNumber = AppUser.PhoneNumber;
            user.Status = AppUser.Status;
            user.Bio = AppUser.Bio;
            user.IsBanned = AppUser.IsBanned;
            user.BanExpirationDate = AppUser.BanExpirationDate;

            // Add roles to user
            foreach (var roleName in UserRolesName)
            {
                var hasRole = await _userManager.IsInRoleAsync(user, roleName);

                if (!hasRole)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            // Add badges to user
            foreach (var badgeName in UserBadgesName)
            {
                if (user.UserBadges.Any(i => i.Badge.Name == badgeName))
                {
                    continue;
                }

                var badge = await _context.Badges.FirstAsync(i => i.Name == badgeName);
                user.UserBadges.Add(new UserBadge()
                {
                    Badge = badge
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
