﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EC_WebSite.Models.UserModel;
using EC_WebSite.Models.ForumModel;

namespace EC_WebSite.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, string>
    {
        //public ApplicationDbContext()
        //{            
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        

        public DbSet<ForumHead> ForumHeads { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<FavoriteThread> FavoriteThreads { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Article> Articles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EC_WebSiteDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Changed standart identity table names
            builder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
            builder.Entity<UserRole>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserToken"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

            builder.Entity<User>(entity =>
            {
                entity.HasOne(m => m.ProfilePhoto)
                        .WithOne()
                        .HasForeignKey<User>(m => m.ProfilePhotoId);
            });

            builder.Entity<ForumHead>(entity =>
            {
                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd();

                entity.HasMany(m => m.Boards)
                    .WithOne(m => m.Forum)
                    .HasForeignKey(k => k.ForumId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Board>(entity =>
            {
                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd();

                entity.HasMany(m => m.Threads)
                    .WithOne(m => m.Board)
                    .HasForeignKey(k => k.BoardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Thread>(entity =>
            {
                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd();

                entity.HasMany(m => m.Posts)
                    .WithOne(m => m.Thread)
                    .HasForeignKey(k => k.ThreadId);
                    

                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Threads)
                    .HasForeignKey(k => k.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Post>(entity =>
            {
                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd();

                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Posts)
                    .HasForeignKey(k => k.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Skill>(entity =>
            {
                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd();                
            });

            builder.Entity<FavoriteThread>(entity =>
            {
                entity.HasKey(k => new { k.ThreadId, k.UserId });

                entity.HasOne(m => m.User)
                     .WithMany(m => m.FavoriteThreads)
                     .HasForeignKey(k => k.UserId)
                     .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(m => m.Thread)
                    .WithMany(m => m.FavoriteThreads)
                    .HasForeignKey(k => k.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<UserSkill>(entity =>
            {
                entity.ToTable("UserSkills");

                entity.HasKey(k => new { k.SkillId, k.UserId });

                entity.HasOne(m => m.Skill)
                    .WithMany(m => m.UserSkills)
                    .HasForeignKey(k => k.SkillId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.User)
                    .WithMany(m => m.UserSkills)
                    .HasForeignKey(k => k.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Article>(entity =>
            {
                entity.HasOne(m => m.Author)
                    .WithOne()
                    .HasForeignKey<Article>(m => m.AuthorId);
            });
        }
    }
}