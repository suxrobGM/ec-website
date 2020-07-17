using EC_Website.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EC_Website.Web.Pages.Admin
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}