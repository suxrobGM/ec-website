using System.Collections.Generic;
using System.Globalization;

namespace EC_Website.Web.ViewModels
{
    public class CultureSwitcherViewModel
    {
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}
