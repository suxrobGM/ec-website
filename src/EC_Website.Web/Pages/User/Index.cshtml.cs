using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.User;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public ApplicationUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = RouteData.Values["username"].ToString();
            AppUser = await _context.Users.FirstOrDefaultAsync(i => i.UserName == username);

            if (AppUser == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}