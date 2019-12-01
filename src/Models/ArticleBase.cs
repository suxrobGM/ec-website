using System;
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
            var url = title.Trim();

            if (useHypen)
                url = url.Replace(' ', '-');
            else
                url = url.Replace(' ', '_');

            url = url.RemoveReservedUrlCharacters();
            url = url.TranslateToLatin();
            url = url.RemoveDiacritics();

            if (useLowerLetters)
                url = url.ToLower();

            return url;
        }
    }
}
