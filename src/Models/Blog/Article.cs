using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;
using EC_WebSite.Models.UserModel;

namespace EC_WebSite.Models.Blog
{
    public class Article
    {
        public Article()
        {
            Id = GeneratorId.GenerateLong();
            CreatedTime = DateTime.Now;
            Comments = new List<Comment>();
        }

        public string Id { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        [Required(ErrorMessage = "Please enter the article topic name")]
        [StringLength(50, ErrorMessage = "Characters must be less than 50")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Article url")]
        public string Url { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required(ErrorMessage = "Please enter the summary of article")]
        [StringLength(200, ErrorMessage = "Characters must be less than 200")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string CoverPhotoUrl { get; set; }
        public int ViewCount { get; set; }
        public DateTime? CreatedTime { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
