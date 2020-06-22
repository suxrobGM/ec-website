﻿// <auto-generated />
using System;
using EC_Website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EC_Website.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200621151616_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EC_Website.Models.Blog.BlogEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPhotoPath")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("LikedUserNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("BlogEntries");
                });

            modelBuilder.Entity("EC_Website.Models.Blog.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("BlogEntryId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogEntryId");

                    b.HasIndex("ParentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.Board", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("ForumId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

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

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.FavoriteThread", b =>
                {
                    b.Property<string>("ThreadId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("ThreadId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteThreads");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.ForumHead", b =>
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

                    b.ToTable("ForumHeads");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThreadId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThreadId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.Thread", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("BoardId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

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

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("EC_Website.Models.UserModel.Badge", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("EC_Website.Models.UserModel.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BanExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)")
                        .HasMaxLength(4096);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("HeaderPhotoPath")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

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
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

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

            modelBuilder.Entity("EC_Website.Models.UserModel.UserBadge", b =>
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

            modelBuilder.Entity("EC_Website.Models.UserModel.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EC_Website.Models.Wikipedia.Category", b =>
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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("WikiCategories");
                });

            modelBuilder.Entity("EC_Website.Models.Wikipedia.WikiEntry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("AuthorId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

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

                    b.ToTable("WikiEntries");
                });

            modelBuilder.Entity("EC_Website.Models.Wikipedia.WikiEntryCategory", b =>
                {
                    b.Property<string>("WikiEntryId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("WikiEntryId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("WikiEntryCategory");
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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
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

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("EC_Website.Models.Blog.BlogEntry", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", "Author")
                        .WithMany("BlogEntries")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("EC_Website.Models.Blog.Comment", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EC_Website.Models.Blog.BlogEntry", "Entry")
                        .WithMany("Comments")
                        .HasForeignKey("BlogEntryId");

                    b.HasOne("EC_Website.Models.Blog.Comment", "Parent")
                        .WithMany("Replies")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.Board", b =>
                {
                    b.HasOne("EC_Website.Models.ForumModel.ForumHead", "Forum")
                        .WithMany("Boards")
                        .HasForeignKey("ForumId");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.FavoriteThread", b =>
                {
                    b.HasOne("EC_Website.Models.ForumModel.Thread", "Thread")
                        .WithMany("FavoriteThreads")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Models.UserModel.User", "User")
                        .WithMany("FavoriteThreads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.Post", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EC_Website.Models.ForumModel.Thread", "Thread")
                        .WithMany("Posts")
                        .HasForeignKey("ThreadId");
                });

            modelBuilder.Entity("EC_Website.Models.ForumModel.Thread", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("AuthorId");

                    b.HasOne("EC_Website.Models.ForumModel.Board", "Board")
                        .WithMany("Threads")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("EC_Website.Models.UserModel.UserBadge", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.Badge", "Badge")
                        .WithMany("UserBadges")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Models.UserModel.User", "User")
                        .WithMany("UserBadges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EC_Website.Models.Wikipedia.WikiEntry", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", "Author")
                        .WithMany("WikiEntries")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("EC_Website.Models.Wikipedia.WikiEntryCategory", b =>
                {
                    b.HasOne("EC_Website.Models.Wikipedia.Category", "Category")
                        .WithMany("WikiEntryCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Models.Wikipedia.WikiEntry", "Entry")
                        .WithMany("WikiEntryCategories")
                        .HasForeignKey("WikiEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EC_Website.Models.UserModel.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EC_Website.Models.UserModel.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
