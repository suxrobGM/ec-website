using System.Collections.Generic;

namespace EC_Website.Core.Entities.Wikipedia
{
    public class WikiEntry : ArticleBase
    {
        public virtual ICollection<WikiEntryCategory> WikiEntryCategories { get; set; } = new List<WikiEntryCategory>();
    }
}
