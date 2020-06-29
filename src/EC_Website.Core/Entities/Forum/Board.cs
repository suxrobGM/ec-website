using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC_Website.Core.Entities.Forum
{
    public class Board : EntityBase
    {
        [Required(ErrorMessage = "Please enter the board name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }

        [StringLength(80)]
        public string Slug { get; set; }

        [StringLength(32)]
        public string ForumId { get; set; }
        public virtual ForumHead Forum { get; set; }

        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
    }
}
