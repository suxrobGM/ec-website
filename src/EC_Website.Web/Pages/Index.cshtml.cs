using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SuxrobGM.Sdk.AspNetCore.Pagination;
using EC_Website.Core.Interfaces.Repositories;

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
            var blogs = _repository.GetAll<Core.Entities.BlogModel.Blog>().OrderByDescending(i => i.Timestamp);
            Blogs = await PaginatedList<Core.Entities.BlogModel.Blog>.CreateAsync(blogs, pageIndex, 5);
            return Page();
        }
    }
}