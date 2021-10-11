using Auth.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
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
