using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;
using EC_Website.Models.ForumModel;
using EC_Website.Models.Blog;
using EC_Website.Models.Wikipedia;

// ReSharper disable once CheckNamespace
namespace EC_Website.Models.UserModel
{
    public class User : IdentityUser
    {
        public User()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Id = GeneratorId.GenerateComplex();
            Timestamp = DateTime.Now;
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }           
        public bool IsBanned { get; set; }
        public DateTime? BanExpirationDate { get; set; }
        public DateTime Timestamp { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string HeaderPhotoUrl { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();
        public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; } = new List<FavoriteThread>();
        public virtual ICollection<BlogArticle> BlogArticles { get; set; } = new List<BlogArticle>();
        public virtual ICollection<UserLikedBlogArticle> LikedArticles { get; set; } = new List<UserLikedBlogArticle>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<WikiArticle> WikiArticles { get; set; } = new List<WikiArticle>();

        public override string ToString() => UserName;
    }
}
