using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces;
using EC_Website.Web.ViewModels;

namespace EC_Website.Web.Pages.Forums.Search
{
    public class PostModel : PageModel
    {
        public readonly IForumRepository _forumRepository;

        public PostModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty(SupportsGet = true)]
        public SearchViewModel SearchViewModel { get; set; } 
        public PaginatedList<Post> Posts { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] SearchViewModel filter, int pageIndex = 1)
        {
            IList<Post> posts;
            SearchViewModel = filter;

            if (filter.SearchString == null)
            {
                posts = await _forumRepository.GetListAsync<Post>();
            }
            else
            {
                posts = await _forumRepository.GetListAsync<Post>(i => i.Content.ToLower().Contains(filter.SearchString.ToLower()));
            }
            
            Posts = PaginatedList<Post>.Create(posts, pageIndex);
            return Page();
        }
    }
}