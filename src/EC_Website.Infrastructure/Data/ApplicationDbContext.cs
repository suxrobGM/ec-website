using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EC_Website.Core.Entities.BlogModel;
using EC_Website.Core.Entities.ForumModel;
using EC_Website.Core.Entities.UserModel;
using EC_Website.Core.Entities.WikiModel;

namespace EC_Website.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, string>
    {
        public ApplicationDbContext()
        {            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Forum> Forums { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<WikiPage> WikiPages { get; set; }
        public DbSet<Category> WikiCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV14;Database=EC_WebsiteDB; AttachDbFilename=C:\Users\suxrobgm\Databases\EC_WebsiteDB.mdf; Trusted_Connection=True;MultipleActiveResultSets=true")
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

                entity.HasMany(m => m.FavoriteThreads)
                    .WithOne();
            });

            builder.Entity<Forum>(entity =>
            {
                entity.HasMany(m => m.Boards)
                    .WithOne(m => m.Forum);
            });

            builder.Entity<Board>(entity =>
            {
                entity.HasMany(m => m.Threads)
                    .WithOne(m => m.Board);
            });

            builder.Entity<Thread>(entity =>
            {
                entity.HasMany(m => m.Posts)
                    .WithOne(m => m.Thread);
            });

            builder.Entity<UserBadge>(entity =>
            {
                entity.HasOne(m => m.Badge)
                    .WithMany(m => m.UserBadges);

                entity.HasOne(m => m.User)
                    .WithMany(m => m.UserBadges);
            });

            builder.Entity<Blog>(entity =>
            {
                entity.HasMany(m => m.Comments)
                    .WithOne(m => m.Blog);

                entity.HasMany(m => m.LikedUsers)
                    .WithOne();

                entity.HasIndex(m => m.Slug)
                    .IsUnique();
            });

            builder.Entity<BlogTag>(entity =>
            {
                entity.HasOne(m => m.Blog)
                    .WithMany(m => m.BlogTags);
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
                entity.HasIndex(m => m.Name)
                    .IsUnique();
            });

            builder.Entity<WikiPageCategory>(entity =>
            {
                entity.HasOne(m => m.WikiPage)
                    .WithMany(m => m.WikiPageCategories);

                entity.HasOne(m => m.Category)
                    .WithMany(m => m.WikiPageCategories);
            });
        }
    }
}
