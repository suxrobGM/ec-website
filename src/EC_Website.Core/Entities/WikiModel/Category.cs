using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.WikiModel
{
    public class Category : EntityBase
    {
        [Required(ErrorMessage = "Please enter the category name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(80)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }
        public virtual ICollection<WikiPageCategory> WikiPageCategories { get; set; } = new List<WikiPageCategory>();

        public override string ToString() => Name;
    }
}
