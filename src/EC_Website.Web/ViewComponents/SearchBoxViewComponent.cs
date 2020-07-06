using Microsoft.AspNetCore.Mvc;
using EC_Website.Web.ViewModels;

namespace EC_Website.Web.ViewComponents
{
    public class SearchBoxViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var viewModel = new SearchViewModel();
            return View(viewModel);
        }
    }
}
