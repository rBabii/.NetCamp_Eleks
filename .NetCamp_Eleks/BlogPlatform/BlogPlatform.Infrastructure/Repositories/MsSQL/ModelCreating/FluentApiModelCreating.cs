using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
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
                .HasKey(b => b.BlogId);

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.BlogUrl)
                .IsUnique();

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.UserId)
                .IsUnique();
        }

        public static void BuildPostModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Post>()
                .HasIndex(p => p.BlogId);

            modelBuilder.Entity<Post>()
                .HasOne<Blog>(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId);
        }
    }
}
