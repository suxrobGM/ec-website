using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EC_WebSite.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        

        public DbSet<ForumHeader> ForumHeaders { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EC_WebSiteDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ForumHeader>(entity =>
            {
                entity.HasMany(m => m.Boards)
                    .WithOne(m => m.Forum)
                    .HasForeignKey(k => k.ForumId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Board>(entity =>
            {              
                entity.HasMany(m => m.Threads)
                    .WithOne(m => m.Board)
                    .HasForeignKey(k => k.BoardId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Thread>(entity =>
            {                
                entity.HasMany(m => m.Posts)
                    .WithOne(m => m.Thread)
                    .HasForeignKey(k => k.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Threads)
                    .HasForeignKey(k => k.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Post>(entity =>
            {
                entity.HasOne(m => m.Author)
                    .WithMany(m => m.Posts)
                    .HasForeignKey(k => k.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
