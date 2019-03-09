using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Data;

namespace EC_WebSite.Pages.Forums.Board
{
    public class CreateBoardModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateBoardModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public string ForumName { get; set; }

        [BindProperty]
        public Models.ForumModel.Board Board { get; set; }

        public IActionResult OnGet()
        {
            var forumHeadId = RouteData.Values["forumHeadId"].ToString();
            var forum = _db.ForumHeads.Where(i => i.Id == forumHeadId).FirstOrDefault();
            ForumName = forum.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var forumHeadId = RouteData.Values["forumHeadId"].ToString();
            Board.Forum = _db.ForumHeads.Where(i => i.Id == forumHeadId).FirstOrDefault();

            _db.Boards.Add(Board);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Forums/Index");
        }
    }
}