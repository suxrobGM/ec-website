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
        public IActionResult CreateBoard(string forumHeaderId)
        {
            using (var db = new ApplicationDbContext())
            {
                var forum = db.ForumHeaders.Where(i => i.Id == forumHeaderId).FirstOrDefault();
                var model = new CreateBoardViewModel() { Forum = forum };
                return View(model);
            }         
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
        public IActionResult CreateBoard(CreateBoardViewModel model, string forumHeaderId)
        {
            using (var db = new ApplicationDbContext())
            {
                if (model.Forum == null)
                    model.Forum = db.ForumHeaders.Where(i => i.Id == forumHeaderId).FirstOrDefault();

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