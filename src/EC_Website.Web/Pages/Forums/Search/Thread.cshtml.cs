using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Interfaces.Repositories;
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

        public IActionResult OnGet([FromQuery] SearchViewModel filter, int pageIndex = 1)
        {
            var threads = _forumRepository.GetAll<Core.Entities.ForumModel.Thread>();
            SearchViewModel = filter;

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                threads = threads.Where(i => i.Title.ToLower().Contains(filter.SearchString.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.UserName))
            {
                threads = threads.Where(i => i.Author.UserName.ToLower().Contains(filter.UserName.ToLower()));
            }

            threads = filter.TimeFrame switch
            {
                SearchTimeFrame.LastDay => threads.Where(i => i.Timestamp >= DateTime.Today.AddDays(-1)),
                SearchTimeFrame.LastWeek => threads.Where(i => i.Timestamp >= DateTime.Today.AddDays(-7)),
                SearchTimeFrame.LastMonth => threads.Where(i => i.Timestamp >= DateTime.Today.AddMonths(-1)),
                SearchTimeFrame.LastYear => threads.Where(i => i.Timestamp >= DateTime.Today.AddYears(-1)),
                _ => threads
            };

            Threads = PaginatedList<Core.Entities.ForumModel.Thread>.Create(threads, pageIndex, 25);
            return Page();
        }
    }
}