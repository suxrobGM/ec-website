using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using Microsoft.AspNetCore.Identity;

namespace EC_Website.Pages.Admin.Users
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.UserModel.User> _userManager;

        public DetailsModel(ApplicationDbContext context,
            UserManager<Models.UserModel.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Models.UserModel.User AppUser { get; set; }
        public IList<string> UserRoles { get; set; }  

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

            UserRoles = await _userManager.GetRolesAsync(AppUser);

            return Page();
        }
    }
}
