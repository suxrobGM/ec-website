using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.Forum;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EC_Website.Web.Pages.Forums
{
    public class ForumsIndexModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ForumsIndexModel(IForumRepository forumRepository,
            UserManager<ApplicationUser> userManager)
        {
            _forumRepository = forumRepository;
            _userManager = userManager;
        }
       
        public string SearchText { get; set; }
        public IList<ForumHead> ForumHeads { get; set; }
        public IList<FavoriteThread> FavoriteThreads { get; set; }       

        public async Task<IActionResult> OnGetAsync()
        {           
            var user = await _userManager.GetUserAsync(User);
            ForumHeads = await _forumRepository.GetListAsync<ForumHead>();
            FavoriteThreads = user.FavoriteThreads.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteForumHeadAsync(string forumId)
        {
            var forum = await _forumRepository.GetByIdAsync<ForumHead>(forumId);
            await _forumRepository.DeleteForumAsync(forum);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteBoardAsync(string boardId)
        {
            var board = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Board>(boardId);
            await _forumRepository.DeleteBoardAsync(board);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string threadId)
        {
            var favoriteThread = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Thread>(threadId);
            await _forumRepository.DeleteFavoriteThreadAsync(favoriteThread);
            return RedirectToPage("./Index");
        }
    }
}