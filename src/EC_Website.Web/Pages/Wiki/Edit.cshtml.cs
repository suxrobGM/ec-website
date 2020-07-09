using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;

        public EditModel(IRepository repository)
        {
            _repository = repository;
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

            WikiPage = await _repository.GetByIdAsync<WikiPage>(id);

            if (WikiPage == null)
            {
                return NotFound();
            }

            if (WikiPage.Slug == "Economic_Crisis_Wiki")
            {
                IsMainPage = true;
            }

            var categories = await _repository.GetListAsync<Core.Entities.WikiModel.Category>();
            SelectedCategories = WikiPage.WikiPageCategories.Where(i => i.WikiPage.Id == WikiPage.Id).Select(i => i.Category.Name).ToArray();

            ViewData.Add("categories", categories.Select(i => i.Name));
            ViewData.Add("toolbar", new[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontName", "FontSize", "FontColor", "BackgroundColor",
                "LowerCase", "UpperCase", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|",
                "CreateTable", "CreateLink", "Image", "|", "ClearFormat",
                "SourceCode", "FullScreen", "|", "Undo", "Redo"
            });         
            return Page();
        }
     
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var wikiPage = await _repository.GetByIdAsync<WikiPage>(WikiPage.Id);

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

                var category = await _repository.GetAsync<Core.Entities.WikiModel.Category>(i => i.Name == categoryName);

                wikiPage.WikiPageCategories.Add(new WikiPageCategory()
                {
                    Category = category,
                });
            }

            await _repository.UpdateAsync(wikiPage);
            return RedirectToPage("./Index", new { slug = wikiPage.Slug });
        }
    }
}
