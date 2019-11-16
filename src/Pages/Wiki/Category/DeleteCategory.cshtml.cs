using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EC_Website.Data;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Pages.Wiki.Category
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly EC_Website.Data.ApplicationDbContext _context;

        public DeleteCategoryModel(EC_Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Wikipedia.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.WikiCategories.FindAsync(id);

            if (Category != null)
            {
                _context.WikiCategories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./List");
        }
    }
}
