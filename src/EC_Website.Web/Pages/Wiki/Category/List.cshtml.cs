using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class CategoriesListModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public CategoriesListModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        public IList<Core.Entities.WikiModel.Category> Categories { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _wikiRepository.GetListAsync<Core.Entities.WikiModel.Category>();
            return Page();
        }
    }
}
