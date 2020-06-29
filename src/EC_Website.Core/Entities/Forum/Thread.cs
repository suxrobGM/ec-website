﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.User;

namespace EC_Website.Core.Entities.Forum
{
    public class Thread : EntityBase
    {
        [Required(ErrorMessage = "Please enter the topic name")]
        [StringLength(80, ErrorMessage = "Characters must be less than 80")]
        public string Title { get; set; }

        [StringLength(80)]
        public string Slug { get; set; }
        public bool IsPinned { get; set; }
        public bool IsLocked { get; set; }

        [StringLength(32)]
        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

        [StringLength(32)]
        public string BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; } = new List<FavoriteThread>();
    }
}