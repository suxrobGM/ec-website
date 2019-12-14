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
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; } = new List<ArticleCategory>();

        public override string ToString() => Name;
    }
}
