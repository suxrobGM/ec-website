using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Wikipedia;
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
        public WikiEntry WikiEntry { get; set; }

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

            WikiEntry = await _repository.GetByIdAsync<WikiEntry>(id);

            if (WikiEntry == null)
            {
                return NotFound();
            }

            if (WikiEntry.Slug == "Economic_Crisis_Wiki")
            {
                IsMainPage = true;
            }

            var categories = await _repository.GetListAsync<Core.Entities.Wikipedia.Category>();
            SelectedCategories = WikiEntry.WikiEntryCategories.Where(i => i.WikiEntryId == WikiEntry.Id).Select(i => i.Category.Name).ToArray();

            ViewData.Add("categories", categories);
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

            var wikiEntry = await _repository.GetByIdAsync<WikiEntry>(WikiEntry.Id);

            if (wikiEntry == null)
            {
                return NotFound();
            }

            // Main page slug must not be changed
            wikiEntry.Slug = !IsMainPage ? ArticleBase.CreateSlug(wikiEntry.Title, false, false) : "Economic_Crisis_Wiki";

            foreach (var categoryName in SelectedCategories)
            {
                if (wikiEntry.WikiEntryCategories.Any(i => i.Category.Name == categoryName)) 
                    continue;

                var category = await _repository.GetAsync<Core.Entities.Wikipedia.Category>(i => i.Name == categoryName);

                wikiEntry.WikiEntryCategories.Add(new WikiEntryCategory()
                {
                    Category = category,
                });
            }

            await _repository.UpdateAsync(wikiEntry);
            return RedirectToPage("./Index", new { slug = wikiEntry.Slug });
        }
    }
}
