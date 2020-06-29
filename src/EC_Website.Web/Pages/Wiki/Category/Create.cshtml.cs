﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using EC_Website.Core.Entities;
using EC_Website.Infrastructure.Data;

namespace EC_Website.Web.Pages.Wiki.Category
{
    [Authorize(Roles = "SuperAdmin,Admin,Moderator,Developer,Editor")]
    public class CreateCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Core.Entities.Wikipedia.Category Category { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category.Slug = ArticleBase.CreateSlug(Category.Name, false, false);
            _context.WikiCategories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./List");
        }
    }
}