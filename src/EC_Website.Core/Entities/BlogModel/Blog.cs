﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EC_Website.Core.Entities.Base;
using EC_Website.Core.Entities.UserModel;

namespace EC_Website.Core.Entities.BlogModel
{
    public class Blog : ArticleBase
    {
        [Required(ErrorMessage = "Please enter the summary of article")]
        [StringLength(250, ErrorMessage = "Characters must be less than 250")]
        public string Summary { get; set; }
        
        [StringLength(64)]
        public string CoverPhotoPath { get; set; }
        public int ViewCount { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
        public virtual ICollection<ApplicationUser> LikedUsers { get; set; } = new List<ApplicationUser>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}