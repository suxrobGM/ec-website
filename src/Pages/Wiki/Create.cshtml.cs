using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class CreateWikiArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateWikiArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WikiArticle WikiArticle { get; set; }

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

            var articleCategories = new List<ArticleCategory>();
            var author = await _context.Users.FirstAsync(i => i.UserName == User.Identity.Name);
            foreach (var categoryName in SelectedCategories)
            {
                var category = await _context.WikiCategories.FirstAsync(i => i.Name == categoryName);
                articleCategories.Add(new ArticleCategory()
                {
                    Article = WikiArticle,
                    ArticleId = WikiArticle.Id,
                    Category = category,
                    CategoryId = category.Id
                });
            }

            WikiArticle.ArticleCategories = articleCategories;
            WikiArticle.Author = author;

            // Main page slug must be not changed
            if (!IsFirstMainPage)
            {
                WikiArticle.Slug = ArticleBase.CreateSlug(WikiArticle.Title, false, false);
            }
            else
            {
                WikiArticle.Slug = "Economic_Crisis_Wiki";
            }
            
            _context.WikiArticles.Add(WikiArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { slug = WikiArticle.Slug });
        }
    }
}
