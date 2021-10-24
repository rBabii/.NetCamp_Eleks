using Auth.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories.MsSql
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
                foreach(var role in user.Roles)
                {
                    _dataContext.Entry(role).State = EntityState.Added;
                }
            }
            else
            {
                _dataContext.Entry(user).State = EntityState.Modified;
                foreach (var role in user.Roles)
                {
                    _dataContext.Entry(role).State = EntityState.Added;
                }
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

        public User Get(int id)
        {
            return _dataContext.Users.SingleOrDefault(u => u.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _dataContext.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetByRefreshToken(string refreshToken)
        {
            return _dataContext.Users.SingleOrDefault(u => u.RefreshToken == refreshToken);
        }
    }
}
