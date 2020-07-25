using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Entities.WikiModel;
using EC_Website.Core.Interfaces;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Policy = Policies.CanManageWikiPages)]
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
        public WikiPage WikiPage { get; set; }

        [BindProperty]
        public string[] SelectedCategories { get; set; }

        [BindProperty]
        public bool IsFirstMainPage { get; set; }

        public async Task<IActionResult> OnGetAsync(bool firstMainPage = false)
        {
            var categories = await _repository.GetListAsync<Core.Entities.WikiModel.Category>();
            ViewData.Add("categories", categories.Select(i => i.Name));
            ViewData.Add("toolbar", new[]
            {
                "Bold", "Italic", "Underline", "StrikeThrough",
                "FontName", "FontSize", "FontColor", "BackgroundColor", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|", "CreateTable", "CreateLink", "Image", "|", 
                "ClearFormat", "SourceCode", "FullScreen", "|", "Undo", "Redo"
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
                var category = await _repository.GetAsync<Core.Entities.WikiModel.Category>(i => i.Name == categoryName);
                WikiPage.WikiPageCategories.Add(new WikiPageCategory()
                {
                    WikiPage = WikiPage,
                    Category = category
                });
            }

            // Main page slug must be not changed
            WikiPage.Slug = !IsFirstMainPage ? WikiPage.Title.Slugify(false, false) : "Economic_Crisis_Wiki";
            WikiPage.Author = author;

            await _repository.AddAsync(WikiPage);
            return RedirectToPage("./Index", new { slug = WikiPage.Slug });
        }
    }
}
