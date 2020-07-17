using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class DeleteCategoryModel : PageModel
    {
        private readonly IRepository _repository;

        public DeleteCategoryModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public Core.Entities.WikiModel.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _repository.GetByIdAsync<Core.Entities.WikiModel.Category>(id);

            if (Category == null)
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

            Category = await _repository.GetByIdAsync<Core.Entities.WikiModel.Category>(id);
            await _repository.DeleteAsync(Category);
            return RedirectToPage("./List");
        }
    }
}
