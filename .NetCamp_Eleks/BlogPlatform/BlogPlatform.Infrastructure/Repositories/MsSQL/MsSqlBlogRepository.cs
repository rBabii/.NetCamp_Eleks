using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.Repositories.MsSQL
{
    public class MsSqlBlogRepository : IBlogRepository
    {
        private readonly DataContext _dataContext;
        public MsSqlBlogRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Blog AddOrUpdate(Blog blog)
        {
            if (blog.BlogId == default)
            {
                _dataContext.Entry(blog).State = EntityState.Added;
            }
            else
            {
                _dataContext.Entry(blog).State = EntityState.Modified;
            }
            _dataContext.SaveChanges();
            return Get(blog.BlogId);
        }

        public bool Delete(Blog blog)
        {
            _dataContext.Blogs.Remove(blog);
            var res = _dataContext.SaveChanges();
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Blog> Get()
        {
            return _dataContext.Blogs
                .AsNoTracking()
                .Include(b => b.User)
                .ToList();
        }

        public IEnumerable<Blog> SearchBlogs(
                int pageNumber = 1,
                int pageSize = 1,
                int blogId = 0,
                string blogUrl = "",
                string searchText = "",
                bool loadRelatedEntities = false
            )
        {
            if (loadRelatedEntities)
            {
                return _dataContext.Blogs
                        .FromSqlInterpolated($"SELECT * FROM [dbo].[GetBlogs] ({pageNumber}, {pageSize}, {blogId}, {blogUrl}, {searchText})")
                        .Include(b => b.User)
                        .AsNoTracking()
                        .ToList();
            }
            return _dataContext.Blogs
                    .FromSqlInterpolated($"SELECT * FROM [dbo].[GetBlogs] ({pageNumber}, {pageSize}, {blogId}, {blogUrl}, {searchText})")
                    .AsNoTracking()
                    .ToList();
        }

        public Blog Get(int Id)
        {
            return _dataContext.Blogs
                .Include(b => b.User)
                .SingleOrDefault(b => b.BlogId == Id);
        }

        public Blog GetByUrl(string url)
        {
            return _dataContext.Blogs
                .Include(b => b.User)
                .SingleOrDefault(b => b.BlogUrl == url);
        }

        public Blog GetByUserId(int userId)
        {
            return _dataContext.Blogs
                .Include(b => b.User)
                .SingleOrDefault(b => b.UserId == userId);
        }
    }
}
