using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace EC_Website.Pages.Admin.UserRoles
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context, UserManager<Models.UserModel.User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public IList<UserRole> UserRoles { get;set; }
        public UserManager<Models.UserModel.User> UserManager { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            UserRoles = await _context.Roles.ToListAsync();

            return Page();
        }
    }
}
