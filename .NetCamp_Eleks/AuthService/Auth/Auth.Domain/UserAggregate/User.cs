using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.UserAggregate
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public string RefreshToken { get; set; }
        public bool IsVerified { get; set; }
    }
}
