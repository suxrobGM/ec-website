using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models;
using EC_WebSite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EC_WebSite.Controllers
{
    public class ForumsController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ForumsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

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

        [HttpGet]
        [Route("Forums/{boardName}/Create", Order = 1)]
        [Route("Forums/{boardId}/Create", Order = 2)]
        public IActionResult CreateThread(string boardId, string boardName)
        {
            using (var db = new ApplicationDbContext())
            {               
                Board board;
                if (boardId != null)
                    board = db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
                else
                    board = db.Boards.Where(i => i.Name == boardName).FirstOrDefault();

                var model = new CreateThreadViewModel() { Board = board };
                return View(model);
            }
        }

        [HttpGet]
        [Route("Forums/{boardName}", Order = 1)]
        [Route("Forums/{boardId}", Order = 2)]
        public IActionResult ForumBoard(string boardId, string boardName)
        {           
            using (var db = new ApplicationDbContext())
            {
                Board board;
                if(boardId != null)
                    board = db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
                else
                    board = db.Boards.Where(i => i.Name == boardName).FirstOrDefault();

                var model = new ForumBoardViewModel()
                {
                    Board = board,
                    Threads = new List<Thread>(board.Threads)
                };
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
                    Name = model.Board.Name,
                    Forum = model.Forum
                });
                db.SaveChanges();
            }

            return Redirect("/Forums/");
        }

        [HttpPost]
        [Route("Forums/{boardName}/Create", Order = 1)]
        [Route("Forums/{boardId}/Create", Order = 2)]
        public async Task<IActionResult> CreateThread(CreateThreadViewModel model, string boardName, string boardId)
        {
            using (var db = new ApplicationDbContext())
            {
                var author = await _userManager.GetUserAsync(User);
                
                var thread = new Thread()
                {
                    Author = author,
                    Name = model.Topic,
                    Board = model.Board
                };
                thread.Posts.Add(model.Post);
                db.Threads.Add(thread);                              
                db.SaveChanges();

                return Redirect($"/Forums/{model.Board.Name}");
            }           
        }
    }
}