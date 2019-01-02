using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EC_WebSite.Controllers
{
    public class _AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public _AccountController(UserManager<User> userManager, ApplicationDbContext context)
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
            return await Task.Run(async () =>
            {
                if (HttpContext.Request.Form.Files[0] == null)
                    return LocalRedirect("~/Identity/Account/Manage/Index");

                var file = HttpContext.Request.Form.Files[0];
                var currentUser = await _userManager.GetUserAsync(User);

                if (file == null || !file.ContentType.StartsWith("image/"))
                    throw new InvalidOperationException($"Unexpected error occurred uploading photo for user with ID '{currentUser.Id}'.");

                var user = _db.Users.Where(x => x.Id == currentUser.Id).FirstOrDefault();

                using (var image = new MagickImage(file.OpenReadStream()))
                {
                    /*if (image.Height > 225)
                    {
                        int offset = (int)Math.Floor(225.0 / image.Height);
                        image.Resize(offset * image.Width, 225);
                    }*/

                    if (image.Height > 225 || image.Width > 225)
                    {                      
                        image.Resize(225, 225);
                        image.Strip();
                        image.Quality = 100;
                    }
                        
                    user.ProfilePhoto = image.ToByteArray();
                    _db.SaveChanges();
                }

                return LocalRedirect("~/Identity/Account/Manage/Index");
            });
        }
    }
}