using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.UserModel
{
    public class Badge : EntityBase
    {
        [Required(ErrorMessage = "Please enter the badge name")]
        [StringLength(40, ErrorMessage = "Characters must be less than 40")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Characters must be less than 250")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

        public override string ToString() => Name;
    }
}
