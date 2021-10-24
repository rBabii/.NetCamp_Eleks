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
    public class AuthentificateResult : BaseResult
    {
        public User User { get; set; }
        public AuthentificateResult(User user, Error error = null)
            : base(error)
        {
            User = user;
        }
    }
}
