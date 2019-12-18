using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EC_Website.Models.UserModel;
using EC_Website.Models.ForumModel;
using EC_Website.Models.Blog;
using EC_Website.Models.Wikipedia;

namespace EC_Website.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, string>
    {
        public ApplicationDbContext()
        {            
        }

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
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<WikiEntry> WikiEntries { get; set; }
        public DbSet<Category> WikiCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=EC_WebsiteDB;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserToken"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
                entity.Property(m => m.Id)
                    .HasMaxLength(32);

                entity.Property(m => m.UserName)
                    .HasMaxLength(32);

                entity.Property(m => m.NormalizedUserName)
                    .HasMaxLength(32);

                entity.Property(m => m.PhoneNumber)
                    .HasMaxLength(32);
            });

            builder.Entity<ForumHead>(entity =>
            {
                entity.HasMany(m => m.Boards)
                    .WithOne(m => m.Forum)
                    .HasForeignKey(k => k.ForumId);
            });

            builder.Entity<Board>(entity =>
            {
                entity.HasMany(m => m.Threads)
                    .WithOne(m => m.Board)
                    .HasForeignKey(k => k.BoardId);
            });

            builder.Entity<Thread>(entity =>
            {                
                entity.HasMany(m => m.Posts)
                    .WithOne(m => m.Thread)
                    .HasForeignKey(k => k.ThreadId);

                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Threads)
                    .HasForeignKey(k => k.AuthorId);
            });

            builder.Entity<Post>(entity =>
            {
                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Posts)
                    .HasForeignKey(k => k.AuthorId);
            });
            

            builder.Entity<FavoriteThread>(entity =>
            {
                entity.HasKey(k => new { k.ThreadId, k.UserId });

                entity.HasOne(m => m.User)
                     .WithMany(m => m.FavoriteThreads)
                     .HasForeignKey(k => k.UserId);

                entity.HasOne(m => m.Thread)
                    .WithMany(m => m.FavoriteThreads)
                    .HasForeignKey(k => k.ThreadId);
            });

            builder.Entity<UserSkill>(entity =>
            {
                entity.HasKey(k => new { k.SkillId, k.UserId });

                entity.HasOne(m => m.Skill)
                    .WithMany(m => m.UserSkills)
                    .HasForeignKey(k => k.SkillId);

                entity.HasOne(m => m.User)
                    .WithMany(m => m.UserSkills)
                    .HasForeignKey(k => k.UserId);
            });

            builder.Entity<BlogEntry>(entity =>
            {
                entity.HasOne(m => m.Author)
                    .WithMany(m => m.BlogEntries)
                    .HasForeignKey(m => m.AuthorId);

                entity.HasMany(m => m.Comments)
                    .WithOne(m => m.Entry)
                    .HasForeignKey(m => m.BlogEntryId);

                entity.Property(m => m.LikedUserNames)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

                entity.Property(m => m.Tags)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

                entity.HasIndex(m => m.Slug)
                    .IsUnique();
            });

            builder.Entity<Comment>(entity =>
            {
                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Comments)
                    .HasForeignKey(m => m.AuthorId);

                entity.HasMany(m => m.Replies)
                    .WithOne(m => m.Parent)
                    .HasForeignKey(m => m.ParentId);
            });

            builder.Entity<WikiEntry>(entity =>
            {
                entity.HasIndex(m => m.Slug)
                    .IsUnique();

                entity.HasOne(m => m.Author)
                    .WithMany(m => m.WikiEntries)
                    .HasForeignKey(m => m.AuthorId);
            });

            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(m => m.Name)
                    .IsUnique();
            });

            builder.Entity<WikiEntryCategory>(entity =>
            {
                entity.ToTable("WikiEntryCategory");
                entity.HasKey(k => new { ArticleId = k.WikiEntryId, k.CategoryId });

                entity.HasOne(m => m.Entry)
                    .WithMany(m => m.WikiEntryCategories)
                    .HasForeignKey(m => m.WikiEntryId);

                entity.HasOne(m => m.Category)
                    .WithMany(m => m.WikiEntryCategories)
                    .HasForeignKey(m => m.CategoryId);
            });
        }
    }
}
