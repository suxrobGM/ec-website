using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Data;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Pages.Wiki
{
    public class CreateWikiArticleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateWikiArticleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {      
            return Page();
        }

        [BindProperty]
        public WikiArticle WikiArticle { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.WikiArticles.Add(WikiArticle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
