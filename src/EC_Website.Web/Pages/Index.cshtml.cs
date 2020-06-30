﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.Pagination;
using EC_Website.Core.Entities.Blog;
using EC_Website.Core.Interfaces;

namespace EC_Website.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public PaginatedList<BlogEntry> BlogEntries { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var blogs = _repository.GetQuery<BlogEntry>(disableTracking: false).OrderByDescending(i => i.Timestamp);
            BlogEntries = await PaginatedList<BlogEntry>.CreateAsync(blogs, pageIndex, 5);
            return Page();
        }
    }
}