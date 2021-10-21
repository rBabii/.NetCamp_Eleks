using Auth.Application.Result;
using Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class ResetPasswordResult : BaseResult
    {
        public ResetPasswordResult(Error error = null)
            : base(error)
        {

        }
    }
}
