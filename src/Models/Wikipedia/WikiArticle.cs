using System.Collections.Generic;

namespace EC_Website.Models.Wikipedia
{
    public class WikiArticle : ArticleBase
    {
        public WikiArticle() : base()
        {
            WikiArticleCategories = new List<WikiArticleCategory>();
        }

        public ICollection<WikiArticleCategory> WikiArticleCategories { get; set; }
    }
}
