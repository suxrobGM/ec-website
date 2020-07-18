using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.ForumModel
{
    public class Board : EntityBase
    {
        [Required(ErrorMessage = "Please enter the board name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(80)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
