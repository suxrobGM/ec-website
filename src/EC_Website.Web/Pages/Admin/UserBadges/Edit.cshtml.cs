using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Admin.UserBadges
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class EditModel : PageModel
    {
        private readonly IRepository<Badge> _repository;

        public EditModel(IRepository<Badge> repository)
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

            Badge = await _repository.GetByIdAsync(id);

            if (Badge == null)
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

            var badge = await _repository.GetByIdAsync(Badge.Id);

            if (badge == null)
            {
                return NotFound();
            }

            badge.Name = Badge.Name;
            badge.Description = Badge.Description;
            await _repository.UpdateAsync(badge);
            return RedirectToPage("./Index");
        }
    }
}
