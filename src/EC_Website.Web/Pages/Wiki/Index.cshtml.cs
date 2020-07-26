using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces.Repositories;

namespace EC_Website.Web.Pages.Wiki
{
    public class WikiArticleIndexModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public WikiArticleIndexModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        public bool IsMainPage { get; set; }
        public WikiPage WikiPage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            if (articleSlug == null)
            {
                return NotFound("Wiki page does not found");
            }

            articleSlug = articleSlug.ToLower();
            WikiPage = await _wikiRepository.GetAsync<WikiPage>(i => i.Slug.ToLower() == articleSlug);

            switch (WikiPage)
            {
                case null when articleSlug == "Economic_Crisis_Wiki".ToLower():
                    return RedirectToPage("/Wiki/Create", new { firstMainPage = true });
                case null:
                    return NotFound($"Wiki page with slug '{articleSlug}' does not found");
            }

            if (articleSlug == "Economic_Crisis_Wiki".ToLower())
            {
                IsMainPage = true;
            }

            return Page();
        }
    }
}
