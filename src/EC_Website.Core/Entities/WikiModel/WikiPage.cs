using System.Collections.Generic;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.WikiModel
{
    public class WikiPage : ArticleBase
    {
        public virtual ICollection<WikiPageCategory> WikiPageCategories { get; set; } = new List<WikiPageCategory>();
    }
}
