using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.UserModel;

namespace EC_Website.Pages.Admin.UserRoles
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserRole UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (UserRole == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
