using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC_Website.Models.Blog
{
    public class BlogEntry : ArticleBase
    {
        [Required(ErrorMessage = "Please enter the summary of article")]
        [StringLength(256, ErrorMessage = "Characters must be less than 256")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
        
        [StringLength(64)]
        public string CoverPhotoPath { get; set; }
        public int ViewCount { get; set; }
        public virtual ICollection<string> Tags { get; set; } = new List<string>();
        public virtual ICollection<string> LikedUserNames { get; set; } = new List<string>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
