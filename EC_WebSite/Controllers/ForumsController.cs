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

        [HttpGet]
        public IActionResult CreateForum()
        {
            var model = new CreateForumViewModel();           
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateBoard()
        {
            var model = new CreateBoardViewModel();           
            return View(model);
        }


        [HttpPost]
        public IActionResult CreateForum(CreateForumViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                db.ForumHeaders.Add(new ForumHeader() { Name = model.ForumName });
                db.SaveChanges();
            }

            return Redirect("/Forums/");
        }

        [HttpPost]
        public IActionResult CreateBoard(CreateBoardViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Boards.Add(new Board()
                {
                    Name = model.BoardName,
                    Forum = model.Forum
                });
                db.SaveChanges();
            }

            return Redirect("/Forums/");
        }
    }
}