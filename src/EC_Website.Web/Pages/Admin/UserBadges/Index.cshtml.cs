using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.User;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Admin.UserBadges
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Badge> Badges { get;set; }

        public async Task OnGetAsync()
        {
            Badges = await _context.Badges.ToListAsync();
        }
    }
}
