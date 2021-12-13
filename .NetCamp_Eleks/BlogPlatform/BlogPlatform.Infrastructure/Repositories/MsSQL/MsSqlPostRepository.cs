using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.Repositories.MsSQL
{
    public class MsSqlPostRepository : IPostRepository
    {
        private readonly DataContext _dataContext;
        public MsSqlPostRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Post AddOrUpdate(Post post)
        {
            if(post.PostId == default)
            {
                _dataContext.Entry(post).State = EntityState.Added;
            }
            else
            {
                _dataContext.Entry(post).State = EntityState.Modified;
            }
            _dataContext.SaveChanges();
            return Get(post.PostId);
        }

        public bool Delete(Post post)
        {
            _dataContext.Posts.Remove(post);
            var res = _dataContext.SaveChanges();
            if(res > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Post> Get()
        {
            return _dataContext.Posts
                .ToList();
        }

        public IEnumerable<Post> SearchPosts(
            int pageNumber = 1, 
            int pageSize = 1, 
            string blogUrl = "", 
            string searchText = "", 
            bool loadRelatedEntities = false
            )
        {
            if (loadRelatedEntities)
            {
                return _dataContext
                    .Posts
                    .FromSqlInterpolated($"SELECT * FROM [dbo].[GetPosts]({pageNumber}, {pageSize}, {blogUrl}, {searchText})")
                    .Include(p => p.Blog)
                    .Include(p => p.Blog.User)
                    .AsNoTracking()
                    .ToList();
            }
            return _dataContext
                    .Posts
                    .FromSqlInterpolated($"SELECT * FROM [dbo].[GetPosts]({pageNumber}, {pageSize}, {blogUrl}, {searchText})")
                    .AsNoTracking()
                    .ToList();

        }

        public Post Get(int Id)
        {
            return _dataContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Blog.User)
                .SingleOrDefault(p => p.PostId == Id);
        }

        public IEnumerable<Post> GetByBlogId(int blogId)
        {
            return _dataContext.Posts
                .Where(p => p.BlogId == blogId);
        }

        public IEnumerable<Post> GetByBlogUrl(string blogUrl)
        {
            return _dataContext.Posts
                .Include(p => p.Blog)
                .Include(p => p.Blog.User)
                .Where(p => p.Blog.BlogUrl == blogUrl);
        }
    }
}
