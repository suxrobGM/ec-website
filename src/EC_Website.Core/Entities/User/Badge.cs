using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC_Website.Core.Entities.User
{
    public class Badge : EntityBase
    {
        [Required(ErrorMessage = "Please enter the badge name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Characters must be less than 250")]
        public string Description { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

        public override string ToString()
        {
            return Name;
        }
    }
}
