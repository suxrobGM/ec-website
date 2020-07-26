using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;

namespace EC_Website.Web.Pages.Forums.Board
{
    public class IndexModel : PageModel
    {
        private readonly IForumRepository _forumRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IForumRepository forumRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _forumRepository = forumRepository;
            _userManager = userManager;
        }
        
        public ApplicationUser AppUser { get; set; }
        public Core.Entities.ForumModel.Board Board { get; set; }
        public PaginatedList<Core.Entities.ForumModel.Thread> PagedThreads { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var boardSlug = RouteData.Values["slug"].ToString();
            AppUser = await _userManager.GetUserAsync(User);
            Board = await _forumRepository.GetAsync<Core.Entities.ForumModel.Board>(i => i.Slug == boardSlug);
            
            if (Board == null)
            {
                return NotFound();
            }

            PagedThreads = PaginatedList<Core.Entities.ForumModel.Thread>.Create(Board.Threads, pageIndex, 25);
            return Page();
        }

        public async Task<IActionResult> OnPostAddToFavoriteThreadsAsync(string threadId)
        {
            var user = await _userManager.GetUserAsync(User);
            var thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(threadId);

            if (thread == null)
            {
                return NotFound();
            }

            await _forumRepository.AddFavoriteThreadAsync(thread, user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string threadId)
        {
            var thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(threadId);
            var user = await _userManager.GetUserAsync(User);
            await _forumRepository.RemoveFavoriteThreadAsync(thread, user);
            return RedirectToPage();
        }
    }
}