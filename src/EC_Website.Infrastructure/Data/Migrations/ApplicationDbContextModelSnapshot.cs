﻿// <auto-generated />
using System;
using EC_Website.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EC_Website.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.Blog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPhotoPath")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<int>("ViewCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Blog");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.BlogLike", b =>
                {
                    b.Property<string>("BlogId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("BlogId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("BlogLike");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.BlogTag", b =>
                {
                    b.Property<string>("BlogId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("TagId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("BlogId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("BlogTag");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("BlogId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.HasIndex("ParentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Board", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("ForumId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("ForumId");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.FavoriteThread", b =>
                {
                    b.Property<string>("ThreadId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("ThreadId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteThread");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Forum", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Forum");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThreadId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThreadId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Thread", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("BoardId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPinned")
                        .HasColumnType("bit");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BoardId");

                    b.ToTable("Thread");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.UserModel.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BanExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("HeaderPhotoPath")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePhotoPath")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.UserModel.Badge", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Badge");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.UserModel.UserBadge", b =>
                {
                    b.Property<string>("BadgeId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("BadgeId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBadge");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.UserModel.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.WikiModel.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.WikiModel.WikiPage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Slug")
                        .IsUnique()
                        .HasFilter("[Slug] IS NOT NULL");

                    b.ToTable("WikiPage");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.WikiModel.WikiPageCategory", b =>
                {
                    b.Property<string>("WikiPageId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("WikiPageId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("WikiPageCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.Blog", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.BlogLike", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.BlogModel.Blog", "Blog")
                        .WithMany("LikedUsers")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "User")
                        .WithMany("LikedBlogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.BlogTag", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.BlogModel.Blog", "Blog")
                        .WithMany("BlogTags")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Core.Entities.BlogModel.Tag", "Tag")
                        .WithMany("BlogTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EC_Website.Core.Entities.BlogModel.Comment", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("EC_Website.Core.Entities.BlogModel.Blog", "Blog")
                        .WithMany("Comments")
                        .HasForeignKey("BlogId");

                    b.HasOne("EC_Website.Core.Entities.BlogModel.Comment", "Parent")
                        .WithMany("Replies")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Board", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.ForumModel.Forum", "Forum")
                        .WithMany("Boards")
                        .HasForeignKey("ForumId");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.FavoriteThread", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.ForumModel.Thread", "Thread")
                        .WithMany("FavoriteThreads")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "User")
                        .WithMany("FavoriteThreads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Post", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("EC_Website.Core.Entities.ForumModel.Thread", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.ForumModel.Thread", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("EC_Website.Core.Entities.ForumModel.Board", "Board")
                        .WithMany("Threads")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.UserModel.UserBadge", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.Badge", "Badge")
                        .WithMany("UserBadges")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "User")
                        .WithMany("UserBadges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EC_Website.Core.Entities.WikiModel.WikiPage", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("EC_Website.Core.Entities.WikiModel.WikiPageCategory", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.WikiModel.Category", "Category")
                        .WithMany("WikiPageCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Core.Entities.WikiModel.WikiPage", "WikiPage")
                        .WithMany("WikiPageCategories")
                        .HasForeignKey("WikiPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EC_Website.Core.Entities.UserModel.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
