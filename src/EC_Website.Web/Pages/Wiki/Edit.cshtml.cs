using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
    public class EditModel : PageModel
    {
        private readonly IWikiRepository _wikiRepository;

        public EditModel(IWikiRepository wikiRepository)
        {
            _wikiRepository = wikiRepository;
        }

        [BindProperty]
        public WikiPage WikiPage { get; set; }

        [BindProperty]
        public string[] SelectedCategories { get; set; }

        [BindProperty]
        public bool IsMainPage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiPage = await _wikiRepository.GetByIdAsync<WikiPage>(id);

            if (WikiPage == null)
            {
                return NotFound();
            }

            if (WikiPage.Slug == "Economic_Crisis_Wiki")
            {
                IsMainPage = true;
            }

            var categories = await _wikiRepository.GetListAsync<Core.Entities.WikiModel.Category>();
            SelectedCategories = WikiPage.WikiPageCategories.Where(i => i.WikiPage.Id == WikiPage.Id).Select(i => i.Category.Name).ToArray();

            ViewData.Add("categories", categories.Select(i => i.Name));
            ViewData.Add("toolbar", new[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontName", "FontSize", "FontColor", "BackgroundColor", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|", "CreateTable", "CreateLink", "Image", "|", 
                "ClearFormat", "SourceCode", "FullScreen", "|", "Undo", "Redo"
            });         
            return Page();
        }
     
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var wikiPage = await _wikiRepository.GetByIdAsync<WikiPage>(WikiPage.Id);

            if (wikiPage == null)
            {
                return NotFound();
            }

            // Main page slug must not be changed
            wikiPage.Slug = !IsMainPage ? wikiPage.Title.Slugify(false, false) : "Economic_Crisis_Wiki";

            foreach (var categoryName in SelectedCategories)
            {
                if (wikiPage.WikiPageCategories.Any(i => i.Category.Name == categoryName)) 
                    continue;

                var category = await _wikiRepository.GetAsync<Core.Entities.WikiModel.Category>(i => i.Name == categoryName);

                wikiPage.WikiPageCategories.Add(new WikiPageCategory()
                {
                    Category = category,
                });
            }

            await _wikiRepository.UpdateAsync(wikiPage);
            return RedirectToPage("./Index", new { slug = wikiPage.Slug });
        }
    }
}
