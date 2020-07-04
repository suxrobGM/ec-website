using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki
{
    public class WikiArticleIndexModel : PageModel
    {
        private readonly IRepository _repository;

        public WikiArticleIndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public bool IsMainPage { get; set; }
        public WikiPage WikiPage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var articleSlug = RouteData.Values["slug"].ToString();
            WikiPage = await _repository.GetAsync<WikiPage>(i => i.Slug == articleSlug);

            switch (WikiPage)
            {
                case null when articleSlug == "Economic_Crisis_Wiki":
                    return RedirectToPage("/Wiki/Create", new { firstMainPage = true });
                case null:
                    return NotFound($"Wiki page with slug '{articleSlug}' does not found");
            }

            if (articleSlug == "Economic_Crisis_Wiki")
            {
                IsMainPage = true;
            }

            return Page();
        }
    }
}
