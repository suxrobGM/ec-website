using System;

namespace EC_Website.Models.Wikipedia
{
    public class WikiArticleCategory
    {
        public string WikiArticleId { get; set; }
        public virtual WikiArticle WikiArticle { get; set; }

        public string CategoryId { get; set; }
        public virtual ArticlesCategory ArticlesCategory { get; set; }
    }
}
