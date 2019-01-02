using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.Forums
{
    public class ThreadModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public ThreadModel(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Models.Thread Thread { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public string SearchText { get; set; }
        public string SelectedPostId { get; set; }
        public string NewPostText { get; set; }

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public void OnGet()
        {

        }
    }
}