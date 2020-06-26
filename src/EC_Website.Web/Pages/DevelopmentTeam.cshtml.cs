using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.User;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages
{
    public class DevelopmentTeamModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DevelopmentTeamModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<ApplicationUser> Developers { get; set; }

        public async Task OnGetAsync()
        {
            var devRole = await _context.Roles.FirstAsync(i => i.Role == Role.Developer);
            var developersUserIds = _context.UserRoles.Where(i => i.RoleId == devRole.Id).Select(i => i.UserId);
            Developers = await _context.Users.Where(i => developersUserIds.Contains(i.Id)).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}