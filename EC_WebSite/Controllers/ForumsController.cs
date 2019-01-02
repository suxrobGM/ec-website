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
    [Route("[controller]/[action]")]
    public class _ForumsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;

        public _ForumsController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _db = context;
        }

        // HTTP GET
        #region Get requests
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userFavoriteThreads = _db.FavoriteThreads.Where(i => i.UserId == currentUser.Id);

            var model = new IndexViewModel()
            {
                ForumHeads = _db.ForumHeads,
                FavoriteThreads = userFavoriteThreads 
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
        public IActionResult CreateBoard(string forumHeadId)
        {
            var forum = _db.ForumHeads.Where(i => i.Id == forumHeadId).FirstOrDefault();
            var model = new CreateBoardViewModel()
            {
                Forum = forum
            };
            return View(model);
        }

        [HttpGet]               
        public IActionResult CreateThread(string boardId)
        {                      
            var board = _db.Boards.Where(i => i.Id == boardId).FirstOrDefault();
            var model = new CreateThreadViewModel() { Board = board };        
            return View(model);
        }

        [HttpGet]               
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
        public IActionResult Thread(string threadId)
        {
            var posts = _db.Posts.Where(i => i.ThreadId == threadId);
            var thread = _db.Threads.Where(i => i.Id == threadId).FirstOrDefault();

            var model = new ThreadViewModel(_userManager)
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

            return Redirect($"/Forums/Thread?threadId={thread.Id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(ThreadViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = _db.Threads.Where(i => i.Id == model.Thread.Id).FirstOrDefault();
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

            return Redirect($"/Forums/Thread?threadId={thread.Id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForumHead(IndexViewModel model)
        {
            await Task.Run(() =>
            {
                var forumHead = _db.ForumHeads.Where(i => i.Id == model.SelectedForumHeadId).FirstOrDefault();

                foreach (var board in forumHead.Boards)
                {
                    foreach (var posts in board.Threads.Select(i => i.Posts))
                    {
                        _db.RemoveRange(posts);
                    }

                    _db.Remove(board);
                }
                
                _db.Remove(forumHead);
                _db.SaveChanges();
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBoard(IndexViewModel model)
        {
            await Task.Run(() =>
            {
                var board = _db.Boards.Where(i => i.Id == model.SelectedBoardId).FirstOrDefault();

                foreach (var posts in board.Threads.Select(i => i.Posts))
                {
                    _db.RemoveRange(posts);
                }

                _db.Remove(board);
                _db.SaveChanges();
            });
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(ThreadViewModel model)
        {
            var post = _db.Posts.Where(i => i.Id == model.SelectedPostId).FirstOrDefault();
            _db.Posts.Remove(post);
            _db.SaveChanges();

            return Redirect($"/Forums/Thread?threadId={model.Thread.Id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFavoriteThread(BoardViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var thread = _db.Threads.Where(i => i.Id == model.SelectedThreadId).FirstOrDefault();

            var favoriteThread = new FavoriteThread()
            {
                Thread = thread,
                User = currentUser
            };

            try
            {
                _db.FavoriteThreads.Add(favoriteThread);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return Redirect($"/Forums/Board?boardId={model.Board.Id}");
            }                               

            return Redirect($"/Forums/Board?boardId={model.Board.Id}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromFavoriteThreads(IndexViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var favoriteThread = _db.FavoriteThreads.Where(i => i.ThreadId == model.SelectedFavoriteThreadId).FirstOrDefault();

            _db.FavoriteThreads.Remove(favoriteThread);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}