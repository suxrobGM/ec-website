using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;

namespace EC_Website.Web.Pages.Forums
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
        
        public IList<Forum> Forums { get; set; }
        public IList<FavoriteThread> FavoriteThreads { get; set; }       

        public async Task<IActionResult> OnGetAsync()
        {           
            var user = await _userManager.GetUserAsync(User);
            Forums = await _forumRepository.GetListAsync<Forum>();

            if (user != null)
            {
                FavoriteThreads = user.FavoriteThreads.ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFromFavoriteThreadsAsync(string threadId)
        {
            var thread = await _forumRepository.GetByIdAsync<Core.Entities.ForumModel.Thread>(threadId);
            var user = await _userManager.GetUserAsync(User);
            await _forumRepository.RemoveFavoriteThreadAsync(thread, user);
            return RedirectToPage("./Index");
        }
    }
}