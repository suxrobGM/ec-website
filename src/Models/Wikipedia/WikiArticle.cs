using System;

namespace EC_Website.Models.Wikipedia
{
    public class WikiArticle : ArticleBase
    {
        public WikiArticle() : base()
        {
        }

        public void GenerateUrl()
        {
            Url = $"{Id}-{Title.Trim().Replace(' ', '-').ToLower()}";
        }
    }
}
