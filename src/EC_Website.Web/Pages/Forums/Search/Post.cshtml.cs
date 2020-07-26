using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces.Repositories;
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
            var posts = _forumRepository.GetAll<Post>();
            SearchViewModel = filter;

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                posts = posts.Where(i => i.Content.ToLower().Contains(filter.SearchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.UserName))
            {
                posts = posts.Where(i => i.Author.UserName.ToLower().Contains(filter.UserName.ToLower()));
            }

            posts = filter.TimeFrame switch
            {
                SearchTimeFrame.LastDay => posts.Where(i => i.Timestamp >= DateTime.Today.AddDays(-1)),
                SearchTimeFrame.LastWeek => posts.Where(i => i.Timestamp >= DateTime.Today.AddDays(-7)),
                SearchTimeFrame.LastMonth => posts.Where(i => i.Timestamp >= DateTime.Today.AddMonths(-1)),
                SearchTimeFrame.LastYear => posts.Where(i => i.Timestamp >= DateTime.Today.AddYears(-1)),
                _ => posts
            };

            Posts = await PaginatedList<Post>.CreateAsync(posts, pageIndex);
            return Page();
        }
    }
}