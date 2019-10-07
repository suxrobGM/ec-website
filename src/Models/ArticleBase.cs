using System;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.UserModel;

namespace EC_Website.Models
{
    public class ArticleBase
    {
        public ArticleBase()
        {
            Id = GeneratorId.GenerateLong();
            Timestamp = DateTime.Now;
        }

        public string Id { get; set; }
        public string Url { get; protected set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        [Required(ErrorMessage = "Please enter the article topic name")]
        [StringLength(50, ErrorMessage = "Characters must be less than 50")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
