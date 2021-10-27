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
            return _dataContext.Users.SingleOrDefault(u => u.Id == Id);
        }

        public User GetByAuthResourceUserId(int authResourceUserId)
        {
            return _dataContext.Users.SingleOrDefault(u => u.AuthResourceUserId == authResourceUserId);
        }

        public User GetByBlogId(int blogId)
        {
            return _dataContext.Users.SingleOrDefault(u => u.BlogId == blogId);
        }

        public User GetByEmail(string email)
        {
            return _dataContext.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetByPhoneNumber(string phoneNumber)
        {
            return _dataContext.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        }
    }
}
