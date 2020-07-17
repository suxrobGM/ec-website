using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Web.ViewComponents
{
    public class UserBadgesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ApplicationUser user)
        {
            return View(user);
        }
    }
}
