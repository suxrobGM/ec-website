using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Repositories;
using EC_Website.Web.Authorization;

namespace EC_Website.Web.Pages.Admin.UserBadges
{
    [Authorize(Policy = Policies.HasAdminRole)]
    public class IndexModel : PageModel
    {
        private readonly IRepository _repository;

        public IndexModel(IRepository repository)
        {
            _repository = repository;
        }

        public IList<Badge> Badges { get;set; }

        public async Task OnGetAsync()
        {
            Badges = await _repository.GetListAsync<Badge>();
        }
    }
}
