using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.BlogModel
{
    public class Blog : ArticleBase
    {
        public Blog()
        {
            CoverPhotoPath = "/img/ec_background.jpg";
        }

        [Required(ErrorMessage = "Please enter the summary of article")]
        [StringLength(250, ErrorMessage = "Characters must be less than 250")]
        [Display(Name = "Summary")]
        public string Summary { get; set; }
        
        [StringLength(64)]
        [Display(Name = "Cover Photo Path")]
        public string CoverPhotoPath { get; set; }

        [Display(Name = "View Count")]
        public int ViewCount { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
        public virtual ICollection<BlogLike> LikedUsers { get; set; } = new List<BlogLike>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
