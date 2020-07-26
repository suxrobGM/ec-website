using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Interfaces.Entities;

namespace EC_Website.Core.Entities.ForumModel
{
    public class Board : EntityBase, ISlugifiedEntity
    {
        [Required(ErrorMessage = "Please enter the board name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(80)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Is Locked")]
        public bool IsLocked { get; set; }

        public virtual Forum Forum { get; set; }
        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
