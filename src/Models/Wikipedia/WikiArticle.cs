using System.Collections.Generic;

namespace EC_Website.Models.Wikipedia
{
    public class WikiArticle : ArticleBase
    {
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; } = new List<ArticleCategory>();
    }
}
