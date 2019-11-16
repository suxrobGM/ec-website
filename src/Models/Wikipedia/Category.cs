using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Models.Wikipedia
{
    public class Category
    {
        public Category()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
            ArticleCategories = new List<ArticleCategory>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string UrlName { get; private set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }

        public override string ToString() => Name;

        public void SetUrlName()
        {
            UrlName = Name.Trim().Replace(' ', '-');
        }
    }
}
