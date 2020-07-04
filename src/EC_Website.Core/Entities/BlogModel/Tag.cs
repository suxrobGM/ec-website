using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;

namespace EC_Website.Core.Entities.BlogModel
{
    public class Tag : EntityBase
    {
        public Tag()
        {

        }

        public Tag(string tagName)
        {
            Name = tagName;
        }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public virtual BlogTag BlogTags { get; set; }

        public override string ToString() => Name;
        public static implicit operator Tag(string tagName) => new Tag(tagName);
        public static implicit operator string(Tag tag) => tag.Name;
    }
}
