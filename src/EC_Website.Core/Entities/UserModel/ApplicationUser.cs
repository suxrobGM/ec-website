using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Interfaces;

namespace EC_Website.Core.Entities.UserModel
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public ApplicationUser()
        {
            Id = GeneratorId.GenerateComplex();
            Timestamp = DateTime.Now;
        }

        [StringLength(32)]
        public sealed override string Id { get; set; }

        [StringLength(40, ErrorMessage = "Characters must be less than 40")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Characters must be less than 40")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Characters must be less than 100")]
        public string Status { get; set; }

        [StringLength(2000, ErrorMessage = "Characters must be less than 2000")]
        public string Bio { get; set; }           
        public bool IsBanned { get; set; }
        public DateTime? BanExpirationDate { get; set; }
        public DateTime Timestamp { get; set; }

        [StringLength(64)]
        public string ProfilePhotoPath { get; set; }

        [StringLength(64)]
        public string HeaderPhotoPath { get; set; }

        public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
        public virtual ICollection<Thread> FavoriteThreads { get; set; } = new List<Thread>();

        public override string ToString() => UserName;
    }
}
