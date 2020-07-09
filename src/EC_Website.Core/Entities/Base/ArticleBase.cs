using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SuxrobGM.Sdk.Extensions;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.Base
{
    public abstract class ArticleBase : EntityBase
    {
        [StringLength(80)]
        public string Slug { get; set; }
        
        [Required(ErrorMessage = "Please enter the article topic name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the article text")]
        public string Content { get; set; }
        public virtual ApplicationUser Author { get; set; }

        
    }
}
