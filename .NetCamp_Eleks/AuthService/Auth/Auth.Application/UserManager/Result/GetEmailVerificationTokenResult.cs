using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class GetEmailVerificationTokenResult : BaseResult
    {
        public string EmailVerificationToken { get; set; }
        public GetEmailVerificationTokenResult(string emailVerificationToken, Error error = null)
            : base(error)
        {
            EmailVerificationToken = emailVerificationToken;
        }
    }
}
