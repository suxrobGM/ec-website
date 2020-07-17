using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki
{
    public class SearchModel : PageModel
    {
        private readonly IRepository _repository;

        public SearchModel(IRepository repository)
        {
            _repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public PaginatedList<WikiPage> WikiPages;

        public IActionResult OnGet(string searchString, int pageIndex = 1)
        {
            var wikiPages = _repository.GetAll<WikiPage>();

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