using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.Repositories.MsSQL.ModelCreating
{
    public static class FluentApiModelCreating
    {
        public static void BuildBlogModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b.BlogId)
                .HasColumnName("b_BlogId");

            modelBuilder.Entity<Blog>()
                .Property(b => b.UserId)
                .HasColumnName("b_UserId");

            modelBuilder.Entity<Blog>()
                .Property(b => b.BlogUrl)
                .HasColumnName("b_BlogUrl")
                .HasMaxLength(150);

            modelBuilder.Entity<Blog>()
                .Property(b => b.DateCreated)
                .HasColumnName("b_DateCreated");

            modelBuilder.Entity<Blog>()
                .Property(b => b.Blocked)
                .HasColumnName("b_Blocked");

            modelBuilder.Entity<Blog>()
                .Property(b => b.Visible)
                .HasColumnName("b_Visible");

            modelBuilder.Entity<Blog>()
                .Property(b => b.Title)
                .HasColumnName("b_Title")
                .HasMaxLength(150);

            modelBuilder.Entity<Blog>()
                .Property(b => b.SubTitle)
                .HasColumnName("b_SubTitle")
                .HasMaxLength(150);

            modelBuilder.Entity<Blog>()
                .Property(b => b.ImageName)
                .HasColumnName("b_ImageName")
                .HasMaxLength(150);

            modelBuilder.Entity<Blog>()
                .Property(b => b.PreviewText)
                .HasColumnName("b_PreviewText")
                .HasMaxLength(150);

            //

            modelBuilder.Entity<Blog>()
                .HasKey(b => b.BlogId);

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.BlogUrl)
                .IsUnique();

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.UserId)
                .IsUnique();
        }
        
        public static void BuildBlogSearchModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogSearch>()
                .Property(bs => bs.BlogSearchId)
                .HasColumnName("bs_BlogSearchId");

            modelBuilder.Entity<BlogSearch>()
                .Property(bs => bs.BlogId)
                .HasColumnName("bs_BlogId");

            modelBuilder.Entity<BlogSearch>()
                .Property(bs => bs.FullBlogText)
                .HasColumnName("bs_FullBlogText");

            //

            modelBuilder.Entity<BlogSearch>()
                .HasKey(bs => bs.BlogSearchId);

            modelBuilder.Entity<BlogSearch>()
                .HasIndex(bs => bs.BlogId)
                .IsUnique();

            modelBuilder.Entity<BlogSearch>()
                .HasOne<Blog>(bs => bs.Blog)
                .WithOne(b => b.BlogSearch)
                .HasForeignKey<BlogSearch>(bs => bs.BlogId);
        }

        public static void BuildPostModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .Property(p => p.PostId)
                .HasColumnName("p_PostId");

            modelBuilder.Entity<Post>()
                .Property(p => p.BlogId)
                .HasColumnName("p_BlogId");

            modelBuilder.Entity<Post>()
                .Property(p => p.DateCreated)
                .HasColumnName("p_DateCreated");

            modelBuilder.Entity<Post>()
                .Property(p => p.DatePosted)
                .HasColumnName("p_DatePosted");

            modelBuilder.Entity<Post>()
                .Property(p => p.Blocked)
                .HasColumnName("p_Blocked");

            modelBuilder.Entity<Post>()
                .Property(p => p.Visible)
                .HasColumnName("p_Visible");

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .HasColumnName("p_Title")
                .HasMaxLength(150);

            modelBuilder.Entity<Post>()
                .Property(p => p.SubTitle)
                .HasColumnName("p_SubTitle")
                .HasMaxLength(150);

            modelBuilder.Entity<Post>()
                .Property(p => p.HeaderContent)
                .HasColumnName("p_HeaderContent");

            modelBuilder.Entity<Post>()
                .Property(p => p.MainContent)
                .HasColumnName("p_MainContent");

            modelBuilder.Entity<Post>()
                .Property(p => p.FooterContent)
                .HasColumnName("p_FooterContent");

            modelBuilder.Entity<Post>()
                .Property(p => p.ImageName)
                .HasColumnName("p_ImageName")
                .HasMaxLength(150);

            modelBuilder.Entity<Post>()
                .Property(p => p.PreviewText)
                .HasColumnName("p_PreviewText")
                .HasMaxLength(150);

            //

            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.BlogId);

            modelBuilder.Entity<Post>()
                .HasOne<Blog>(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId);
        }

        public static void BuildPostSearchModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostSearch>()
                .Property(ps => ps.PostSearchId)
                .HasColumnName("ps_PostSearchId");

            modelBuilder.Entity<PostSearch>()
                .Property(ps => ps.PostId)
                .HasColumnName("ps_PostId");

            modelBuilder.Entity<PostSearch>()
                .Property(ps => ps.FullPostText)
                .HasColumnName("ps_FullPostText");

            //

            modelBuilder.Entity<PostSearch>()
                .HasKey(ps => ps.PostSearchId);

            modelBuilder.Entity<PostSearch>()
                .HasIndex(ps => ps.PostId)
                .IsUnique();

            modelBuilder.Entity<PostSearch>()
                .HasOne<Post>(ps => ps.Post)
                .WithOne(p => p.PostSearch)
                .HasForeignKey<PostSearch>(bs => bs.PostId);
        }

        public static void BuildUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("u_Id");

            modelBuilder.Entity<User>()
                .Property(u => u.AuthResourceUserId)
                .HasColumnName("u_AuthResourceUserId");

            modelBuilder.Entity<User>()
                .Property(u => u.BlogId)
                .HasColumnName("u_BlogId");

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("u_Email")
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasColumnName("u_FirstName")
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasColumnName("u_LastName")
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasColumnName("u_Gender");

            modelBuilder.Entity<User>()
                .Property(u => u.BirthDate)
                .HasColumnName("u_BirthDate");

            modelBuilder.Entity<User>()
                .Property(u => u.PhoneNumber)
                .HasColumnName("u_PhoneNumber")
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(u => u.ImageName)
                .HasColumnName("u_ImageName")
                .HasMaxLength(150);

            modelBuilder.Entity<User>()
                .Property(u => u.IsVerified)
                .HasColumnName("u_IsVerified");

            //

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.AuthResourceUserId)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Blog)
                .WithOne(b => b.User)
                .HasForeignKey<Blog>(b => b.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.IsVerified)
                .HasDefaultValue(false);
        }
    }
}
