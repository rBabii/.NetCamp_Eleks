using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Infrastructure.Repositories.MsSQL
{
    public class MsSqlUserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public MsSqlUserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public User AddOrUpdate(User user)
        {
            if (user.Id == default)
            {
                _dataContext.Entry(user).State = EntityState.Added;
            }
            else
            {
                _dataContext.Entry(user).State = EntityState.Modified;
            }
            _dataContext.SaveChanges();
            return Get(user.Id);
        }

        public bool Delete(User user)
        {
            _dataContext.Users.Remove(user);
            var res = _dataContext.SaveChanges();
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<User> Get()
        {
            return _dataContext.Users.ToList();
        }

        public User Get(int Id)
        {
            return _dataContext.Users
                .Where(u => u.Id == Id)
                .Include(u => u.Blog)
                .SingleOrDefault();
        }

        public User GetByAuthResourceUserId(int authResourceUserId)
        {
            return _dataContext.Users
                .Where(u => u.AuthResourceUserId == authResourceUserId)
                .Include(u => u.Blog)
                .SingleOrDefault();
        }

        public User GetByBlogId(int blogId)
        {
            return _dataContext.Users
                .Where(u => u.BlogId == blogId)
                .Include(u => u.Blog)
                .SingleOrDefault();
        }

        public User GetByEmail(string email)
        {
            return _dataContext.Users
                .Where(u => u.Email == email)
                .Include(u => u.Blog)
                .SingleOrDefault();
        }

        public User GetByPhoneNumber(string phoneNumber)
        {
            return _dataContext.Users
                .Where(u => u.PhoneNumber == phoneNumber)
                .Include(u => u.Blog)
                .SingleOrDefault();
        }
    }
}
