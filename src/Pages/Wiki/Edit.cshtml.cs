using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Pages.Wiki
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
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

            WikiEntry = await _context.WikiEntries
                .Include(w => w.Author).FirstOrDefaultAsync(m => m.Id == id);

            if (WikiEntry == null)
            {
                return NotFound();
            }

            if (WikiEntry.Slug == "Economic_Crisis_Wiki")
            {
                IsMainPage = true;
            }

            var categories = _context.WikiCategories.Select(i => i.Name);
            SelectedCategories = WikiEntry.WikiEntryCategories.Where(i => i.WikiEntryId == WikiEntry.Id).Select(i => i.Category.Name).ToArray();

            ViewData.Add("categories", categories);
            ViewData.Add("toolbar", new string[]
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

            _context.Attach(WikiEntry).State = EntityState.Modified;

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
            WikiEntry.AuthorId = author.Id;

            // Main page slug must be not changed
            if (!IsMainPage)
            {
                WikiEntry.Slug = ArticleBase.CreateSlug(WikiEntry.Title, false, false);
            }
            else
            {
                WikiEntry.Slug = "Economic_Crisis_Wiki";
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WikiArticleExists(WikiEntry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { slug = WikiEntry.Slug });
        }

        private bool WikiArticleExists(string id)
        {
            return _context.WikiEntries.Any(e => e.Id == id);
        }
    }
}
