﻿using System.Threading.Tasks;
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
            var categories = _context.WikiCategories;
            ViewData.Add("categories", categories);
            return Page();
        }

        [BindProperty]
        public WikiArticle WikiArticle { get; set; }
      
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
