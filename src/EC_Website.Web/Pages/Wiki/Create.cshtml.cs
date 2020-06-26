using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities;
using EC_Website.Core.Entities.Wikipedia;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class CreateWikiArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateWikiArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WikiEntry WikiEntry { get; set; }

        [BindProperty]
        public string[] SelectedCategories { get; set; }

        [BindProperty]
        public bool IsFirstMainPage { get; set; }

        public IActionResult OnGet(bool firstMainPage = false)
        {
            var categories = _context.WikiCategories.Select(i => i.Name);
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

            var articleCategories = new List<WikiEntryCategory>();
            var author = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);
            foreach (var categoryName in SelectedCategories)
            {
                var category = await _context.WikiCategories.FirstAsync(i => i.Name == categoryName);
                articleCategories.Add(new WikiEntryCategory()
                {
                    Entry = WikiEntry,
                    WikiEntryId = WikiEntry.Id,
                    Category = category,
                    CategoryId = category.Id
                });
            }

            WikiEntry.WikiEntryCategories = articleCategories;
            WikiEntry.Author = author;

            // Main page slug must be not changed
            if (!IsFirstMainPage)
            {
                WikiEntry.Slug = ArticleBase.CreateSlug(WikiEntry.Title, false, false);
            }
            else
            {
                WikiEntry.Slug = "Economic_Crisis_Wiki";
            }
            
            await _context.WikiEntries.AddAsync(WikiEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { slug = WikiEntry.Slug });
        }
    }
}
