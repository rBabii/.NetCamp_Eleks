using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregate
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(int id);
        User GetByEmail(string email);
        User GetByRefreshToken(string refreshToken);
        User AddOrUpdate(User user);
        bool Delete(User user);
    }
}
