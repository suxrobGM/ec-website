using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using EC_WebSite.Models.ForumModel;
using EC_WebSite.Models.Blog;
using EC_WebSite.Utils;

namespace EC_WebSite.Models.UserModel
{
    public class User : IdentityUser
    {
        public User() : base()
        {
            Id = GeneratorId.GenerateComplex();
            RegistrationDate = DateTime.Now;
            Posts = new List<Post>();
            Threads = new List<Thread>();
            UserSkills = new List<UserSkill>();
            FavoriteThreads = new List<FavoriteThread>();
            Articles = new List<Article>();
            Comments = new List<Comment>();
            CommentReplies = new List<CommentReply>();
        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }
        public string ProfilePhotoId { get; set; }
        public string HeaderPhotoId { get; set; }
        public bool IsBanned { get; set; }
        public DateTime? BanPeriod { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public virtual Media ProfilePhoto { get; set; }
        public virtual Media HeaderPhoto { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
        public virtual ICollection<FavoriteThread> FavoriteThreads { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentReply> CommentReplies { get; set; }
    }
}
