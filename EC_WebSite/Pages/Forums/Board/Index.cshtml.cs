using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.Forums
{
    public class BoardIndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public BoardIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public Models.Board Board { get; set; }
        public string SearchText { get; set; }          


        public IActionResult OnGet()
        {
            var boardId = RouteData.Values["boardId"].ToString();
            Board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

            if (Board.Threads == null)
                Board.Threads = new List<Models.Thread>();                       

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            return RedirectToPage();
        }
    }
}