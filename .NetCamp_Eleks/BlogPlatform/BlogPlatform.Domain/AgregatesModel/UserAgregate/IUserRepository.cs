using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.UserAgregate
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(int Id);
        User GetByBlogId(int blogId);
        User GetByEmail(string email);
        User GetByPhoneNumber(string phoneNumber);
        User GetByAuthResourceUserId(int authResourceUserId);
        User AddOrUpdate(User user);
        bool Delete(User user);
    }
}
