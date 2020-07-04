using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class CategoriesListModel : PageModel
    {
        private readonly IRepository _repository;

        public CategoriesListModel(IRepository repository)
        {
            _repository = repository;
        }

        public IList<Core.Entities.WikiModel.Category> Categories { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _repository.GetListAsync<Core.Entities.WikiModel.Category>();
            return Page();
        }
    }
}
