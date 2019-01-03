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
        public async Task<string> ViewProfilePhoto()
        {
            var currentUser = await _userManager.GetUserAsync(User);            

            return currentUser.ProfilePhoto.GetDataBase64();
        }        
    }
}