using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Admin.UserBadges
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class DeleteModel : PageModel
    {
        private readonly IRepository _repository;

        public DeleteModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Badge Badge { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Badge = await _repository.GetByIdAsync<Badge>(id);

            if (Badge == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(Badge);
            return RedirectToPage("./Index");
        }
    }
}
