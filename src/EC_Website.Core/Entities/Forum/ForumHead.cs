using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC_Website.Core.Entities.Forum
{
    public class ForumHead : EntityBase
    {
        [Required(ErrorMessage = "Please enter the forum head name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }
        public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
