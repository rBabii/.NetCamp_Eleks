using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public class GenerateResetPasswordTokenResult : BaseResult
    {
        public string ResetPasswordToken { get; set; }
        public GenerateResetPasswordTokenResult(string resetPasswordToken, Error error = null)
            : base (error)
        {
            ResetPasswordToken = resetPasswordToken;
        }
    }
}
