using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SuxrobGM.Sdk.Utils;
using EC_WebSite.Models.ForumModel;
using EC_WebSite.Models.Blog;

namespace EC_WebSite.Models.UserModel
{
    public class User : IdentityUser
    {
        public User() : base()
        {
            Id = GeneratorId.GenerateComplex();
            Timestamp = DateTime.Now;
            Posts = new List<Post>();
            Threads = new List<Thread>();
            UserSkills = new List<UserSkill>();
            FavoriteThreads = new List<FavoriteThread>();
            Articles = new List<Article>();
            Comments = new List<Comment>();
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }           
        public bool IsBanned { get; set; }
        public DateTime? BanPeriod { get; set; }
        public DateTime Timestamp { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string HeaderPhotoUrl { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
