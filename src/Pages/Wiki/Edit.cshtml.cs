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
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WikiArticle WikiArticle { get; set; }

        [BindProperty]
        public string[] SelectedCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WikiArticle = await _context.WikiArticles
                .Include(w => w.Author).FirstOrDefaultAsync(m => m.Id == id);

            if (WikiArticle == null)
            {
                return NotFound();
            }

            var categories = _context.WikiCategories.Select(i => i.Name);
            SelectedCategories = WikiArticle.ArticleCategories.Where(i => i.ArticleId == WikiArticle.Id).Select(i => i.Category.Name).ToArray();

            ViewData.Add("categories", categories);
            ViewData.Add("toolbars", new string[]
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

            _context.Attach(WikiArticle).State = EntityState.Modified;

            var articleCategories = new List<ArticleCategory>();
            foreach (var categoryName in SelectedCategories)
            {
                var category = await _context.WikiCategories.Where(i => i.Name == categoryName).FirstAsync();
                articleCategories.Add(new ArticleCategory()
                {
                    Article = WikiArticle,
                    ArticleId = WikiArticle.Id,
                    Category = category,
                    CategoryId = category.Id
                });
            }

            WikiArticle.Slug = ArticleBase.CreateSlug(WikiArticle.Title, false, false);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WikiArticleExists(WikiArticle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { slug = WikiArticle.Slug });
        }

        private bool WikiArticleExists(string id)
        {
            return _context.WikiArticles.Any(e => e.Id == id);
        }
    }
}
