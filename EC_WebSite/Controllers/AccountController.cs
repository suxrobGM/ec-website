using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EC_WebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _db = context;
        }
        
        public IActionResult Index()
        {
            return RedirectToPage("~/Identity/Account/Manage/Index");
        }

        [HttpGet]
        public async Task<FileStreamResult> ViewProfilePhoto()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            MemoryStream ms = new MemoryStream(currentUser.ProfilePhoto);

            return new FileStreamResult(ms, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto()
        {
            var file = HttpContext.Request.Form.Files[0];            
            var currentUser = await _userManager.GetUserAsync(User);

            if (file == null || !file.ContentType.StartsWith("image/"))
                throw new InvalidOperationException($"Unexpected error occurred uploading photo for user with ID '{currentUser.Id}'.");

            var user = _db.Users.Where(x => x.Id == currentUser.Id).FirstOrDefault();
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);

                user.ProfilePhoto = ms.ToArray();
                _db.SaveChanges();
            }

            return LocalRedirect("~/Identity/Account/Manage/Index");
        }
    }
}