using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
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

        public PaginatedList<Core.Entities.BlogModel.Blog> Blogs { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageIndex = 1)
        {
            var blogs = _repository.GetQuery<Core.Entities.BlogModel.Blog>(disableTracking: false).OrderByDescending(i => i.Timestamp);
            Blogs = await PaginatedList<Core.Entities.BlogModel.Blog>.CreateAsync(blogs, pageIndex, 5);
            return Page();
        }
    }
}