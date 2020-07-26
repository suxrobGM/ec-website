using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces.Entities;

namespace EC_Website.Core.Entities.UserModel
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public ApplicationUser()
        {
            Id = GeneratorId.GenerateComplex();
            Timestamp = DateTime.Now;
            ProfilePhotoPath = "/img/default_user_avatar.jpg";
            HeaderPhotoPath = "/img/default_user_header.jpg";
        }

        [StringLength(32)]
        [Display(Name = "ID")]
        public sealed override string Id { get; set; }

        [StringLength(40, ErrorMessage = "Characters must be less than 40")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Characters must be less than 40")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Characters must be less than 100")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [StringLength(2000, ErrorMessage = "Characters must be less than 2000")]
        [Display(Name = "Bio")]
        public string Bio { get; set; }    
        
        [Display(Name = "Is Banned")]
        public bool IsBanned { get; set; }

        [Display(Name = "Ban Expiration Date")]
        public DateTime? BanExpirationDate { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime Timestamp { get; set; }

        [StringLength(64)]
        [Display(Name = "Profile Photo Path")]
        public string ProfilePhotoPath { get; set; }

        [StringLength(64)]
        [Display(Name = "Header Photo Path")]
        public string HeaderPhotoPath { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; } = new List<FavoriteThread>();
        public virtual ICollection<BlogLike> LikedBlogs { get; set; } = new List<BlogLike>();

        public override string ToString() => UserName;
    }
}
