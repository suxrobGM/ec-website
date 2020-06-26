using System.Collections.Generic;
using System.Globalization;

namespace EC_Website.Web.ViewComponents
{
    public class CultureSwitcherModel
    {
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}
