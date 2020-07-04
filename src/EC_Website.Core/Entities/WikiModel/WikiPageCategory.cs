using System.ComponentModel.DataAnnotations;

namespace EC_Website.Core.Entities.WikiModel
{
    public class WikiPageCategory
    {
        [StringLength(32)]
        public string WikiPageId { get; set; }
        public virtual WikiPage WikiPage { get; set; }

        [StringLength(32)]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
