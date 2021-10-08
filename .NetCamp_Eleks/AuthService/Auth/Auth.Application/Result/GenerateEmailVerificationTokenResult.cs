using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public class GenerateEmailVerificationTokenResult : BaseResult
    {
        public string EmailVerificationToken { get; set; }
        public GenerateEmailVerificationTokenResult(string emailVerificationToken, Error error = null)
            : base(error)
        {

        }
    }
}
