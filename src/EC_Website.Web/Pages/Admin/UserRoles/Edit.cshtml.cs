using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Admin.UserRoles
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class EditModel : PageModel
    {
        private readonly RoleManager<UserRole> _roleManager;

        public EditModel(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public UserRole UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserRole = await _roleManager.FindByIdAsync(id);

            if (UserRole == null)
            {
                return NotFound();
            }
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

            var userRole = await _roleManager.FindByIdAsync(UserRole.Id);

            if (userRole == null)
            {
                return NotFound();
            }
            
            userRole.Description = UserRole.Description;
            await _roleManager.UpdateAsync(userRole);
            return RedirectToPage("./Index");
        }
    }
}
