using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.AuthManager.Result
{
    public class SendEmailVerificationTokenResult : BaseResult
    {
        public SendEmailVerificationTokenResult(Error error = null)
            : base(error)
        {

        }
    }
}
