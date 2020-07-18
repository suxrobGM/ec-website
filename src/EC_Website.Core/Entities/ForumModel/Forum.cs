using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.ForumModel
{
    public class Forum : EntityBase
    {
        [Required(ErrorMessage = "Please enter the forum head name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
    }
}
