using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.Forums
{
    public class BoardModel : PageModel
    {
        private ApplicationDbContext _db;

        public BoardModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.Board Board { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public IEnumerable<Models.Thread> Threads { get; set; }


        public class InputModel
        {
            public string SearchText { get; set; }
            public string SelectedThreadId { get; set; }
        }


        public IActionResult OnGet(string boardId)
        {
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

            if (board.Threads == null)
                board.Threads = new List<Models.Thread>();

            Board = board;
            Threads = board.Threads;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {


            return RedirectToPage();
        }
    }
}