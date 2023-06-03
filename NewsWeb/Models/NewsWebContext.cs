using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace NewsWeb.Models
{
    public partial class NewsWebContext : DbContext
    {
        public NewsWebContext()
        {
        }

        public NewsWebContext(DbContextOptions<NewsWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-9M996E6;Database=NewsWeb;Integrated Security=True");
            }
        }

        private void DefautConnection(SqlServerDbContextOptionsBuilder obj)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasColumnName("alias");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .HasColumnName("categoryName");

                entity.Property(e => e.Cover)
                    .HasMaxLength(255)
                    .HasColumnName("cover");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .HasColumnName("icon");

                entity.Property(e => e.Levels).HasColumnName("levels");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(255)
                    .HasColumnName("metaDesc");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(255)
                    .HasColumnName("metaKey");

                entity.Property(e => e.Ordering).HasColumnName("ordering");

                entity.Property(e => e.Parents).HasColumnName("parents");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.Thumb)
                    .HasMaxLength(255)
                    .HasColumnName("thumb");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("postID");

                entity.Property(e => e.Alias)
                    .HasMaxLength(255)
                    .HasColumnName("alias");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .HasColumnName("author");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.Contents).HasColumnName("contents");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createAt");

                entity.Property(e => e.IsHot).HasColumnName("isHot");

                entity.Property(e => e.IsNewfeed).HasColumnName("isNewfeed");

                entity.Property(e => e.MetaDesc)
                    .HasMaxLength(255)
                    .HasColumnName("metaDesc");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(255)
                    .HasColumnName("metaKey");

                entity.Property(e => e.MetaTitle)
                    .HasMaxLength(255)
                    .HasColumnName("metaTitle");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.SContent)
                    .HasMaxLength(255)
                    .HasColumnName("sContent");

                entity.Property(e => e.Tag).HasColumnName("tag");

                entity.Property(e => e.Thumb)
                    .HasMaxLength(255)
                    .HasColumnName("thumb");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Post_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.RoleDescription)
                    .HasMaxLength(50)
                    .HasColumnName("roleDescription")
                    .IsFixedLength(true);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.LastLogin)
                    .HasMaxLength(10)
                    .HasColumnName("lastLogin")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .HasColumnName("phoneNumber")
                    .IsFixedLength(true);

                entity.Property(e => e.RoleId).HasColumnName("roleID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
