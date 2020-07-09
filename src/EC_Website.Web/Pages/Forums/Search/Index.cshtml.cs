using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Web.ViewModels;

namespace EC_Website.Web.Pages.Forums.Search
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet([FromQuery] SearchViewModel filter)
        {
            return filter.PageType switch
            {
                SearchPageType.Posts => RedirectToPage("./Post", filter),
                SearchPageType.Threads => RedirectToPage("./Thread", filter),
                _ => Page()
            };
        }
    }
}