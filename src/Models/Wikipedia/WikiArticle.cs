using System.Collections.Generic;

namespace EC_Website.Models.Wikipedia
{
    public class WikiArticle : ArticleBase
    {
        public WikiArticle() : base()
        {
            ArticleCategories = new List<ArticleCategory>();
        }
        
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
