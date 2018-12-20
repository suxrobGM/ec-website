using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using EC_WebSite.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EC_WebSite.Controllers
{
    public class ForumsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ForumsViewModel();
            return View(model);
        }
    }
}