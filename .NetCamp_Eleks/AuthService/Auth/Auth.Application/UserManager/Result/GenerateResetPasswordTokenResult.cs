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
    public class GenerateResetPasswordTokenResult : BaseResult
    {
        public string ResetPasswordToken { get; set; }
        public User User { get; set; }
        public GenerateResetPasswordTokenResult(string resetPasswordToken, User user, Error error = null)
            : base (error)
        {
            ResetPasswordToken = resetPasswordToken;
            User = user;
        }
    }
}
