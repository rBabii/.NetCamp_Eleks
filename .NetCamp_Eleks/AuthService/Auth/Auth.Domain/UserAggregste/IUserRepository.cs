using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregste
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(int id);
        User GetByEmail(string email);
        User GetByUserName(string userName);
        User GetByRefreshToken(string refreshToken);
        User AddOrUpdate(User user);
        void Delete(User user);
    }
}
