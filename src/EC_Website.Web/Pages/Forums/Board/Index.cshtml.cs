using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.User;
using EC_Website.Core.Interfaces;

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
        
        public Core.Entities.Forum.Board Board { get; set; }
        public string SearchText { get; set; }          


        public async Task<IActionResult> OnGetAsync()
        {
            var boardSlug = RouteData.Values["slug"].ToString();
            Board = await _forumRepository.GetAsync<Core.Entities.Forum.Board>(i => i.Slug == boardSlug);

            if (Board == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddToFavoriteThreadsAsync(string threadId)
        {
            var user = await _userManager.GetUserAsync(User);
            var thread = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Thread>(threadId);

            if (thread == null)
            {
                return NotFound();
            }

            await _forumRepository.AddFavoriteThreadAsync(thread, user);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string threadId)
        {
            var favoriteThread = await _forumRepository.GetByIdAsync<Core.Entities.Forum.Thread>(threadId);
            await _forumRepository.DeleteFavoriteThreadAsync(favoriteThread);
            return RedirectToPage();
        }
    }
}