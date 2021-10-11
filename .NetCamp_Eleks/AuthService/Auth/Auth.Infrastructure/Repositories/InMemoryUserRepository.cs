using Auth.Domain.UserAggregate;
using Auth.Domain.UserAggregate.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories
{
    public class InMemoryUserRepository: IUserRepository
    {
        private readonly List<User> Users = new List<User>();
        private bool IsModifyLocked = false;

        public User AddOrUpdate(User user)
        {
            while (IsModifyLocked)
            {
                Thread.Sleep(100);
            }
            IsModifyLocked = true;
            if(user == null)
            {
                IsModifyLocked = false;
                throw new ArgumentNullException("user object argument can not be null.");
            }
            if(string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.UserName))
            {
                IsModifyLocked = false;
                throw new ArgumentException("Email, Password and UserName is required fields in User object.");
            }
            if( user.Id < 1 ) //Add new
            {
                if(Users.Count < 1)
                {
                    user.Id = 1;
                    Users.Add(user);

                    var addedUser = Get(user.Id);
                    if(addedUser == null)
                    {
                        IsModifyLocked = false;
                        throw new UserAddException("User is not added, or was deleted by another process.");
                    }
                    IsModifyLocked = false;
                    return addedUser;
                }
                else
                {
                    user.Id = (Users.Last().Id + 1);
                    Users.Add(user);
                    var addedUser = Get(user.Id);
                    if(addedUser == null)
                    {
                        IsModifyLocked = false;
                        throw new UserAddException("User is not added, or was deleted by another process.");
                    }
                    IsModifyLocked = false;
                    return addedUser;
                }
            }
            // Update existing
            var index = Users.FindIndex(_user => _user.Id == user.Id);
            if(index < 0)
            {
                IsModifyLocked = false;
                throw new UserUpdateException("user not found for updating.");
            }
            Users[index] = user;
            var updatedUser = Get(user.Id);
            if(updatedUser == null)
            {
                IsModifyLocked = false;
                throw new UserUpdateException("user not found after updating process.");
            }
            IsModifyLocked = false;
            return updatedUser;
        }

        public bool Delete(User user)
        {
            while (IsModifyLocked)
            {
                Thread.Sleep(100);
            }
            IsModifyLocked = true;
            if (user == null)
            {
                IsModifyLocked = false;
                return false;
            }
            var removeResult = Users.Remove(user);
            IsModifyLocked = false;
            return removeResult;
        }

        public IEnumerable<User> Get()
        {
            return Users.OrderBy(user => user.Id);
        }

        public User Get(int id)
        {
            if(id < 1)
            {
                return null;
            }
            var user = Users.Where(user => user.Id == id)?.SingleOrDefault();
            return user;
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }
            var user = Users.Where(user => user.Email == email)?.SingleOrDefault();
            return user;
        }

        public User GetByRefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return null;
            }
            var user = Users.Where(user => user.RefreshToken == refreshToken)?.SingleOrDefault();
            return user;
        }

        public User GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }
            var user = Users.Where(user => user.UserName == userName)?.SingleOrDefault();
            return user;
        }
    }
}
