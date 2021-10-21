using Auth.Application.Result;
using Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class LogoutResult : BaseResult
    {
        public LogoutResult(Error error = null)
            : base(error)
        {
        }
    }
}
