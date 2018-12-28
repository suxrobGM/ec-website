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
        private readonly ApplicationDbContext _db;

        public ForumsController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _db = context;
        }

        // HTTP GET
        #region Get requests
        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                ForumHeaders = _db.ForumHeads
            };
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
            var forum = _db.ForumHeads.Where(i => i.Id == forumHeaderId).FirstOrDefault();
            var model = new CreateBoardViewModel()
            {
                Forum = forum
            };
            return View(model);
        }

        [HttpGet]        
        [Route("Forums/{boardId}/Create")]
        public IActionResult CreateThread(string boardId)
        {                      
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
            var model = new CreateThreadViewModel() { Board = board };        
            return View(model);
        }

        [HttpGet]        
        [Route("Forums/{boardId}")]
        public IActionResult Board(string boardId)
        {                    
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
           
            if (board.Threads == null)
                board.Threads = new List<Thread>();

            var model = new BoardViewModel()
            {
                Board = board,
                Threads = board.Threads
            };

            return View(model);
        }

        [HttpGet]
        [Route("Forums/Thread/{threadId}")]
        public IActionResult Thread(string threadId)
        {
            var posts = _db.Posts.Where(i => i.ThreadId == threadId);
            var thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();

            var model = new ThreadViewModel()
            {
                Thread = thread,
                Posts = posts
            };

            return View(model);
        }
        #endregion


        // HTTP POST
        #region Post requests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateForum(CreateForumViewModel model)
        {
            _db.ForumHeads.Add(new ForumHead() { Name = model.ForumName });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBoard(CreateBoardViewModel model)
        {
            var forum = _db.ForumHeads.Where(i => i.Id == model.Forum.Id).FirstOrDefault();

            _db.Boards.Add(new Board()
            {
                Name = model.Board.Name,
                Forum = forum
            });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]        
        [Route("Forums/{boardId}/Create")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateThread(CreateThreadViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var author = _db.Users.Where(i => i.Id == currentUser.Id).FirstOrDefault();
            var board = _db.Boards.Where(i => i.Id == model.Board.Id).FirstOrDefault();

            var thread = new Thread()
            {
                Author = author,
                Name = model.Topic,
                Board = board
            };

            var post = new Post()
            {
                Author = author,
                Text = model.Text,
                Thread = thread,
                CreatedTime = DateTime.Now
            };

            thread.Posts.Add(post);

            _db.Threads.Add(thread);
            _db.Posts.Add(post);
            _db.SaveChanges();

            return Redirect($"/Forums/{model.Board.Id}");
        }

        [HttpPost]        
        [Route("Forums/Thread/{threadId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(ThreadViewModel model, string threadId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();
            var author = _db.Users.Where(i => i.Id == currentUser.Id).FirstOrDefault();

            var post = new Post()
            {
                Author = author,
                Thread = thread,
                Text = model.NewPostText,
                CreatedTime = DateTime.Now
            };

            _db.Posts.Add(post);
            _db.SaveChanges();

            return Redirect($"/Forums/Thread/{thread.Id}");
        }

        /*[HttpDelete]
        public async Task<IActionResult> DeleteForumHeader(IndexViewModel model)
        {

            return RedirectToAction("Index");
        }*/
        #endregion
    }
}