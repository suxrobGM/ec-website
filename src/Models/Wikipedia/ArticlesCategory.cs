using System;
using System.Collections.Generic;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Models.Wikipedia
{
    public class ArticlesCategory
    {
        public ArticlesCategory()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
            WikiArticleCategories = new List<WikiArticleCategory>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<WikiArticleCategory> WikiArticleCategories { get; set; }
    }
}
