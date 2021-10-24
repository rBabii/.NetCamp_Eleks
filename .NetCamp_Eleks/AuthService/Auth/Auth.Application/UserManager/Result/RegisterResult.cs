using Auth.Domain.UserAggregate;
using Auth.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using External.Result.Base;

namespace Auth.Application.UserManager.Result
{
    public class RegisterResult : BaseResult
    {
        public User User { get; set; }
        public string EmailVerifyToken { get; set; }
        public RegisterResult(User user, string emailVerifyToken, Error error = null)
            : base(error)
        {
            EmailVerifyToken = emailVerifyToken;
            User = user;
        }
    }
}
