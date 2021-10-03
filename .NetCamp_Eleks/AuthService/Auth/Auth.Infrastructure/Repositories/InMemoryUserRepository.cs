using Auth.Domain.UserAggregste;
using Auth.Domain.UserAggregste.Exceptions;
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
                    try
                    {
                        var addedUser = Get(user.Id);
                        IsModifyLocked = false;
                        return addedUser;
                    }
                    catch (UserNotFoundException ex)
                    {
                        IsModifyLocked = false;
                        throw new UserAddException("User is not added, or was deleted by another process.", ex);
                    }
                    catch (Exception)
                    {
                        IsModifyLocked = false;
                        throw;
                    }
                }
                else
                {
                    user.Id = (Users.Last().Id + 1);
                    try
                    {
                        var addedUser = Get(user.Id);
                        IsModifyLocked = false;
                        return addedUser;
                    }
                    catch (UserNotFoundException ex)
                    {
                        IsModifyLocked = false;
                        throw new UserAddException("User is not added, or was deleted by another process.", ex);
                    }
                    catch (Exception)
                    {
                        IsModifyLocked = false;
                        throw;
                    }
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
            try
            {
                IsModifyLocked = false;
                return Get(user.Id);
            }
            catch (UserNotFoundException ex)
            {
                IsModifyLocked = false;
                throw new UserUpdateException("user not found after updating process.", ex);
            }
            catch(Exception)
            {
                IsModifyLocked = false;
                throw;
            }
        }

        public void Delete(User user)
        {
            while (IsModifyLocked)
            {
                Thread.Sleep(100);
            }
            IsModifyLocked = true;
            if (user == null)
            {
                IsModifyLocked = false;
                throw new ArgumentNullException("user object argument can not be null.");
            }
            var removeResult = Users.Remove(user);
            if (!removeResult)
            {
                IsModifyLocked = false;
                throw new UserDeleteException("user was not found to deleted.");
            }
            IsModifyLocked = false;
        }

        public IEnumerable<User> Get()
        {
            return Users.OrderBy(user => user.Id);
        }

        public User Get(int id)
        {
            if(id < 1)
            {
                throw new ArgumentException("id argument can not be less then '1'.");
            }
            var user = Users.Where(user => user.Id == id)?.SingleOrDefault();
            if(user == null)
            {
                throw new UserNotFoundException($"user with id:'{id}' is not found.");
            }
            return user;
        }

        public User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("email argument can not be null or empty string.");
            }
            var user = Users.Where(user => user.Email == email)?.SingleOrDefault();
            if(user == null)
            {
                throw new UserNotFoundException($"user with email:'{email}' is not found.");
            }
            return user;
        }

        public User GetByRefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException("refreshToken argument can not be null or empty string.");
            }
            var user = Users.Where(user => user.RefreshToken == refreshToken)?.SingleOrDefault();
            if(user == null)
            {
                throw new UserNotFoundException($"user with refreshToken:'{refreshToken}' is not found.");
            }
            return user;
        }

        public User GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("userName argument can not be null or empty string.");
            }
            var user = Users.Where(user => user.UserName == userName)?.SingleOrDefault();
            if (user == null)
            {
                throw new UserNotFoundException($"user with userName:'{userName}' is not found.");
            }
            return user;
        }
    }
}
