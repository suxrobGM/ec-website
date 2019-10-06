using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EC_Website.Models.Blog
{
    public class BlogArticle : ArticleBase
    {
        public BlogArticle() : base()
        {
            Comments = new List<Comment>();
        }

        [Required]
        public string Tags { get; set; }

        [Required(ErrorMessage = "Please enter the summary of article")]
        [StringLength(200, ErrorMessage = "Characters must be less than 200")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        [DataType(DataType.MultilineText)]
        public string CoverPhotoUrl { get; set; }
        public int ViewCount { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public string GetRelativeUrl()
        {
            return Url.Remove(0, "/Article/".Length);
        }
    }
}
