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
                if (user.Roles != null)
                {
                    foreach (var role in user.Roles)
                    {
                        if(role.Id == default)
                        {
                            _dataContext.Entry(role).State = EntityState.Added;

                        }
                        else
                        {
                            _dataContext.Entry(role).State = EntityState.Modified;
                        }
                    }
                }
            }
            else
            {
                _dataContext.Entry(user).State = EntityState.Modified;
                if(user.Roles != null)
                {
                    foreach (var role in user.Roles)
                    {
                        if (role.Id == default)
                        {
                            _dataContext.Entry(role).State = EntityState.Added;

                        }
                        else
                        {
                            _dataContext.Entry(role).State = EntityState.Modified;
                        }
                    }
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
            var res = _dataContext.Users.Where(u => u.Id == id).Include(u => u.Roles).SingleOrDefault();
            return res;
        }

        public User GetByEmail(string email)
        {
            var res = _dataContext.Users.Where(u => u.Email == email).Include(u => u.Roles).SingleOrDefault();
            return res;
        }

        public User GetByRefreshToken(string refreshToken)
        {
            var res = _dataContext.Users.Where(u => u.RefreshToken == refreshToken).Include(u => u.Roles).SingleOrDefault();
            return res;
        }
    }
}
