using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using SuxrobGM.Sdk.Utils;
using SuxrobGM.Sdk.Extensions;
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
        public string Slug { get; set; }
        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        [Required(ErrorMessage = "Please enter the article topic name")]
        [StringLength(50, ErrorMessage = "Characters must be less than 50")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public static string CreateSlug(string title, bool useHypen = true, bool useLowerLetters = true)
        {
            var url = title.RemoveReservedUrlCharacters().TranslateToLatin();
            var words = url.Split().Where(str => !string.IsNullOrWhiteSpace(str));

            if (useHypen)
                url = string.Join('-', words);
            else
                url = string.Join('_', words);

            if (useLowerLetters)
                url = url.ToLower();

            return url;
        }
    }
}
