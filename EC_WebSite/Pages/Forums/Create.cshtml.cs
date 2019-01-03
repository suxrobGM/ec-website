using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.Forums
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string ForumName { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            _db.ForumHeads.Add(new ForumHead() { Name = ForumName });
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}