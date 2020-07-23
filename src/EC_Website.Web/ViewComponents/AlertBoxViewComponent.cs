using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Web.ViewModels;

namespace EC_Website.Web.ViewComponents
{
    public class AlertBoxViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string message, AlertType alertType = AlertType.Success, bool dismissible = true)
        {
            var alertBox = new AlertBoxViewModel(message)
            {
                AlertType = alertType,
                IsDismissible = dismissible
            };
            return View(alertBox);
        }
    }
}
