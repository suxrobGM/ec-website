using System;

namespace EC_Website.Models.Wikipedia
{
    public class WikiArticle : ArticleBase
    {
        public WikiArticle() : base()
        {
        }

        public string GetRelativeUrl()
        {
            return Url.Remove(0, "/Wiki/".Length);
        }
    }
}
