using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_WebSite.Pages.User
{
    public class UserViewIndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public UserViewIndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.UserModel.User UserContext { get; set; }

        public void OnGet()
        {
            string username = User.Identity.Name;

            UserContext = _db.Users.Where(i => i.UserName == username).FirstOrDefault();
        }
    }
}