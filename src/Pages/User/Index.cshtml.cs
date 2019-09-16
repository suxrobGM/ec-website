using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_WebSite.Data;

namespace EC_WebSite.Pages.User
{
    public class UserViewIndexModel : PageModel
    {
        private ApplicationDbContext _db;
        private UserManager<Models.UserModel.User> _userManager;

        public UserViewIndexModel(ApplicationDbContext db, UserManager<Models.UserModel.User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Models.UserModel.User UserContext { get; set; }

        public void OnGet()
        {
            string username = RouteData.Values["username"].ToString();

            UserContext = _db.Users.Where(i => i.UserName == username).FirstOrDefault();
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(Models.UserModel.User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}