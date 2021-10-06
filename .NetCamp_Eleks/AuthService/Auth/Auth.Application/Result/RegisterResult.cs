using Auth.Domain.UserAggregste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public class RegisterResult : BaseResult
    {
        public User User { get; set; }
        public RegisterResult(User user, Error error = null)
            : base(error)
        {
            User = user;
        }
    }
}
