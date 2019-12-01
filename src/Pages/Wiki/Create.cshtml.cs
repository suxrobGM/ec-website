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
    [Authorize(Roles = "SuperAdmin,Admin,Moderator")]
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

        public IActionResult OnGet()
        {
            var categories = _context.WikiCategories.Select(i => i.Name);
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

            var articleCategories = new List<ArticleCategory>();
            var author = await _context.Users.Where(i => i.UserName == User.Identity.Name).FirstAsync();
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

            WikiArticle.ArticleCategories = articleCategories;
            WikiArticle.Author = author;
            WikiArticle.AuthorId = author.Id;
            WikiArticle.Slug = ArticleBase.CreateSlug(WikiArticle.Title, false, false);
            _context.WikiArticles.Add(WikiArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { slug = WikiArticle.Slug });
        }
    }
}
