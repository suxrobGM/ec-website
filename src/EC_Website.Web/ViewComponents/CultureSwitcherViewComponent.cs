using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using EC_Website.Web.ViewModels;

namespace EC_Website.Web.ViewComponents
{
    public class CultureSwitcherViewComponent : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;
        public CultureSwitcherViewComponent(IOptions<RequestLocalizationOptions> localizationOptions) =>
            _localizationOptions = localizationOptions;

        public IViewComponentResult Invoke()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var viewModel = new CultureSwitcherViewModel
            {
                SupportedCultures = _localizationOptions.Value.SupportedUICultures.ToList(),
                CurrentUICulture = cultureFeature.RequestCulture.UICulture
            };

            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureFeature.RequestCulture.Culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), Secure = true }
            );

            return View(viewModel);
        }
    }
}
