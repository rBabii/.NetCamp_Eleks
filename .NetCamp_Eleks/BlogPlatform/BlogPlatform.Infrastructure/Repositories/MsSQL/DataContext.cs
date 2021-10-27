using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using BlogPlatform.Infrastructure.Repositories.MsSQL.ModelCreating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.Repositories.MsSQL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base (options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            FluentApiModelCreating.BuildBlogModel(modelBuilder);
            FluentApiModelCreating.BuildPostModel(modelBuilder);
            FluentApiModelCreating.BuildUserModel(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
        
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
