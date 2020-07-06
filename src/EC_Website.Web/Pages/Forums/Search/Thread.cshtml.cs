using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Interfaces;
using EC_Website.Web.ViewModels;

namespace EC_Website.Web.Pages.Forums.Search
{
    public class ThreadModel : PageModel
    {
        private readonly IForumRepository _forumRepository;

        public ThreadModel(IForumRepository forumRepository)
        {
            _forumRepository = forumRepository;
        }

        [BindProperty(SupportsGet = true)]
        public SearchViewModel SearchViewModel { get; set; } 

        public PaginatedList<Core.Entities.ForumModel.Thread> Threads { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] SearchViewModel filter, int pageIndex = 1)
        {
            IList<Core.Entities.ForumModel.Thread> threads;

            if (filter.SearchString == null)
            {
                threads = await _forumRepository.GetListAsync<Core.Entities.ForumModel.Thread>();
            }
            else
            {
                threads = await _forumRepository.GetListAsync<Core.Entities.ForumModel.Thread>(i => i.Title.ToLower().Contains(filter.SearchString.ToLower()));
            }

            Threads = PaginatedList<Core.Entities.ForumModel.Thread>.Create(threads, pageIndex, 25);
            SearchViewModel = filter;
            return Page();
        }
    }
}