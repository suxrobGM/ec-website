using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class CreateCategoryModel : PageModel
    {
        private readonly IRepository _repository;

        public CreateCategoryModel(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Core.Entities.WikiModel.Category Category { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category.Slug = ArticleBase.CreateSlug(Category.Name, false, false);
            await _repository.AddAsync(Category);
            return RedirectToPage("./List");
        }
    }
}
