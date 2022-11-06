using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Entities.WikiModel;

namespace EC_Website.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, string>
{
    public ApplicationDbContext()
    {            
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserRole>(entity => { entity.ToTable(name: "Role"); });
        builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRole"); });
        builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaim"); });
        builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogin"); });
        builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserToken"); });
        builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaim"); });

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(m => m.Id)
                .HasMaxLength(32);

            entity.Property(m => m.UserName)
                .HasMaxLength(32);

            entity.Property(m => m.NormalizedUserName)
                .HasMaxLength(32);

            entity.Property(m => m.PhoneNumber)
                .HasMaxLength(32);
        });

        builder.Entity<UserBadge>(entity =>
        {
            entity.HasKey(k => new {k.BadgeId, k.UserId});

            entity.HasOne(m => m.Badge)
                .WithMany(m => m.UserBadges)
                .HasForeignKey(m => m.BadgeId);

            entity.HasOne(m => m.User)
                .WithMany(m => m.UserBadges)
                .HasForeignKey(m => m.UserId);
        });

        builder.Entity<Forum>(entity =>
        {
            entity.HasMany(m => m.Boards)
                .WithOne(m => m.Forum);

            entity.HasIndex(m => m.Title)
                .IsUnique();
        });

        builder.Entity<Board>(entity =>
        {
            entity.HasMany(m => m.Threads)
                .WithOne(m => m.Board);

            entity.HasIndex(m => m.Slug)
                .IsUnique();
        });

        builder.Entity<Thread>(entity =>
        {
            entity.HasMany(m => m.Posts)
                .WithOne(m => m.Thread);
        });

        builder.Entity<FavoriteThread>(entity =>
        {
            entity.HasKey(k => new {k.ThreadId, k.UserId});

            entity.HasOne(m => m.Thread)
                .WithMany(m => m.FavoriteThreads)
                .HasForeignKey(m => m.ThreadId);

            entity.HasOne(m => m.User)
                .WithMany(m => m.FavoriteThreads)
                .HasForeignKey(m => m.UserId);
        });

        builder.Entity<Blog>(entity =>
        {
            entity.HasMany(m => m.Comments)
                .WithOne(m => m.Blog);

            entity.HasIndex(m => m.Slug)
                .IsUnique();
        });

        builder.Entity<BlogTag>(entity =>
        {
            entity.HasKey(k => new {k.BlogId, k.TagId});

            entity.HasOne(m => m.Blog)
                .WithMany(m => m.BlogTags)
                .HasForeignKey(m => m.BlogId);

            entity.HasOne(m => m.Tag)
                .WithMany(m => m.BlogTags)
                .HasForeignKey(m => m.TagId);
        });

        builder.Entity<BlogLike>(entity =>
        {
            entity.HasKey(k => new {k.BlogId, k.UserId});

            entity.HasOne(m => m.Blog)
                .WithMany(m => m.LikedUsers)
                .HasForeignKey(m => m.BlogId);

            entity.HasOne(m => m.User)
                .WithMany(m => m.LikedBlogs)
                .HasForeignKey(m => m.UserId);
        });

        builder.Entity<Comment>(entity =>
        {
            entity.HasMany(m => m.Replies)
                .WithOne(m => m.Parent);
        });

        builder.Entity<WikiPage>(entity =>
        {
            entity.HasIndex(m => m.Slug)
                .IsUnique();
        });

        builder.Entity<Category>(entity =>
        {
            entity.HasIndex(m => m.Slug)
                .IsUnique();
        });

        builder.Entity<WikiPageCategory>(entity =>
        {
            entity.HasKey(k => new {k.WikiPageId, k.CategoryId});

            entity.HasOne(m => m.WikiPage)
                .WithMany(m => m.WikiPageCategories)
                .HasForeignKey(m => m.WikiPageId);

            entity.HasOne(m => m.Category)
                .WithMany(m => m.WikiPageCategories)
                .HasForeignKey(m => m.CategoryId);
        });
    }
}