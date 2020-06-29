using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;

namespace EC_Website.Core.Entities.Wikipedia
{
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "Please enter the category name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Name { get; set; }

        [StringLength(80)]
        public string Slug { get; set; }
        public virtual ICollection<WikiEntryCategory> WikiEntryCategories { get; set; } = new List<WikiEntryCategory>();

        public override string ToString() => Name;
    }
}
