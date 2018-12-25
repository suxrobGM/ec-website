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

        public ForumsController(UserManager<User> userManager)
        {
            _userManager = userManager;
            _db = new ApplicationDbContext();
        }

        // HTTP GET
        #region Get requests
        [HttpGet]
        public IActionResult Index()
        {
            var model = new ForumsViewModel();

            var forumHeaders = _db.ForumHeaders.ToList();
            if (forumHeaders.Any())
                model.ForumHeaders = forumHeaders;

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
            var forum = _db.ForumHeaders.Where(i => i.Id == forumHeaderId).FirstOrDefault();
            var model = new CreateBoardViewModel() { Forum = forum };
            return View(model);
        }

        [HttpGet]        
        [Route("Forums/{boardId}/Create")]
        public IActionResult CreateThread(string boardId)
        {                      
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
            
            var model = new CreateThreadViewModel()
            {
                BoardId = board.Id,
                BoardName = board.Name
            };
            return View(model);
        }

        [HttpGet]        
        [Route("Forums/{boardId}")]
        public IActionResult ForumBoard(string boardId)
        {                    
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
           
            if (board.Threads == null)
                board.Threads = new List<Thread>();

            var model = new ForumBoardViewModel()
            {
                Board = board,
                Threads = board.Threads.ToList()
            };

            return View(model);
        }

        [HttpGet]
        [Route("Forums/Thread/{threadId}")]
        public IActionResult ForumThread(string threadId)
        {
            var posts = _db.Posts.Where(i => i.ThreadId == threadId).ToList();
            var thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();
            var model = new ForumThreadViewModel()
            {
                ThreadId = thread.Id,
                ThreadName = thread.Name,
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
            _db.ForumHeaders.Add(new ForumHeader() { Name = model.ForumName });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBoard(CreateBoardViewModel model, string forumHeaderId)
        {
            if (model.Forum == null)
                model.Forum = _db.ForumHeaders.Where(i => i.Id == forumHeaderId).FirstOrDefault();

            _db.Boards.Add(new Board()
            {
                Name = model.Board.Name,
                Forum = model.Forum
            });
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]        
        [Route("Forums/{boardId}/Create")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CreateThread(CreateThreadViewModel model, string boardId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var author = _db.Users.Where(i => i.Id == currentUser.Id).FirstOrDefault();
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();

            var thread = new Thread()
            {
                Author = author,
                Name = model.Topic,
                Board = board,
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

            return Redirect($"/Forums/{model.BoardId}");
        }

        [HttpPost]        
        [Route("Forums/Thread/{threadId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(ForumThreadViewModel model, string threadId)
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

            return Redirect(thread.Id); // Redirect to /Forums/Thread/{thread.Id}
        }
        #endregion
    }
}