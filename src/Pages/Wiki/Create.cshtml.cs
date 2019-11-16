using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Data;
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

        public IActionResult OnGet()
        {
            var categories = _context.WikiCategories;
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

        [BindProperty]
        public WikiArticle WikiArticle { get; set; }
      
        public async Task<IActionResult> OnPostAsync()
        {
            WikiArticle.GenerateUrl();

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.WikiArticles.Add(WikiArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { wikiArticleUrl = WikiArticle.Url });
        }
    }
}
