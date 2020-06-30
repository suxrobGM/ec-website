using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Entities.Wikipedia;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class CreateWikiArticleModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateWikiArticleModel(IRepository repository, 
            UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [BindProperty]
        public WikiEntry WikiEntry { get; set; }

        [BindProperty]
        public string[] SelectedCategories { get; set; }

        [BindProperty]
        public bool IsFirstMainPage { get; set; }

        public async Task<IActionResult> OnGetAsync(bool firstMainPage = false)
        {
            var categories = await _repository.GetListAsync<Core.Entities.Wikipedia.Category>();
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
            IsFirstMainPage = firstMainPage;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var author = await _userManager.GetUserAsync(User);
            foreach (var categoryName in SelectedCategories)
            {
                var category = await _repository.GetAsync<Core.Entities.Wikipedia.Category>(i => i.Name == categoryName);
                WikiEntry.WikiEntryCategories.Add(new WikiEntryCategory()
                {
                    Entry = WikiEntry,
                    Category = category
                });
            }

            // Main page slug must be not changed
            WikiEntry.Slug = !IsFirstMainPage ? ArticleBase.CreateSlug(WikiEntry.Title, false, false) : "Economic_Crisis_Wiki";
            WikiEntry.Author = author;

            await _repository.UpdateAsync(WikiEntry);
            return RedirectToPage("./Index", new { slug = WikiEntry.Slug });
        }
    }
}
