using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Utils;

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
        public string Topic { get; set; }

        [Required(ErrorMessage = "Please enter the summary of article")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        [DataType(DataType.Html)]
        public string Text { get; set; }

        public string CoverPhotoId { get; set; }
        public virtual Media CoverPhoto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedTime { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
