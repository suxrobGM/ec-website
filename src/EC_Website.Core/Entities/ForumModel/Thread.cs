using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Interfaces.Entities;

namespace EC_Website.Core.Entities.ForumModel
{
    public class Thread : EntityBase, ISlugifiedEntity
    {
        [Required(ErrorMessage = "Please enter the topic name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(80)]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Is Pinned")]
        public bool IsPinned { get; set; }

        [Display(Name = "Is Locked")]
        public bool IsLocked { get; set; }

        [Display(Name = "Author")]
        public virtual ApplicationUser Author { get; set; }

        [Display(Name = "Board")]
        public virtual Board Board { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; } = new List<FavoriteThread>();
    }
}
