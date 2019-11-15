using System;

namespace EC_Website.Models.Wikipedia
{
    public class ArticleCategory
    {
        public string ArticleId { get; set; }
        public virtual WikiArticle Article { get; set; }

        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
