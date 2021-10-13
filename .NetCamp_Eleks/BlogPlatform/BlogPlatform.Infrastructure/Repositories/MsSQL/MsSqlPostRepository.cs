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
            return _dataContext.Posts.ToList();
        }

        public Post Get(int Id)
        {
            return _dataContext.Posts.SingleOrDefault(p => p.PostId == Id);
        }

        public IEnumerable<Post> GetByBlogId(int blogId)
        {
            return _dataContext.Posts.Where(p => p.BlogId == blogId);
        }
    }
}
