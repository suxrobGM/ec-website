using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces.Repositories;

namespace EC_Website.Web.Pages.Wiki
{
    public class SearchModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public SearchModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public PaginatedList<WikiPage> WikiPages;

        public IActionResult OnGet(string searchString, int pageIndex = 1)
        {
            var wikiPages = _wikiRepository.GetAll<WikiPage>();

            if (string.IsNullOrEmpty(searchString))
            {
                wikiPages = wikiPages.Where(i => i.Content.Contains(searchString));
            }

            SearchString = searchString;
            WikiPages = PaginatedList<WikiPage>.Create(wikiPages, pageIndex, 20);
            return Page();
        }
    }
}