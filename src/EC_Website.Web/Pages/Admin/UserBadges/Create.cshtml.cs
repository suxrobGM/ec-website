using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Admin.UserBadges
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class CreateModel : PageModel
    {
        private readonly IRepository<Badge> _repository;

        public CreateModel(IRepository<Badge> repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Badge Badge { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.AddAsync(Badge);
            return RedirectToPage("./Index");
        }
    }
}
