using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.User;

namespace EC_Website.Core.Entities
{
    public abstract class ArticleBase : EntityBase
    {
        [StringLength(80)]
        public string Slug { get; set; }

        [StringLength(32)]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        [Required(ErrorMessage = "Please enter the article topic name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public static string CreateSlug(string title, bool useHypen = true, bool useLowerLetters = true)
        {
            var url = title.TranslateToLatin();
            
            // invalid chars           
            url = Regex.Replace(url, @"[^A-Za-z0-9\s-]", "");

            // convert multiple spaces into one space 
            url = Regex.Replace(url, @"\s+", " ").Trim();
            var words = url.Split().Where(str => !string.IsNullOrWhiteSpace(str));
            url = string.Join(useHypen ? '-' : '_', words);

            if (useLowerLetters)
                url = url.ToLower();

            return url;
        }
    }
}
