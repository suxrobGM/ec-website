using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;

namespace EC_Website.Pages.User
{
    public class UserViewIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public UserViewIndexModel(ApplicationDbContext context, UserManager<Models.UserModel.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Models.UserModel.User UserContext { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = RouteData.Values["username"].ToString();
            UserContext = await _context.Users.FirstOrDefaultAsync(i => i.UserName == username);

            if (UserContext == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(Models.UserModel.User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}