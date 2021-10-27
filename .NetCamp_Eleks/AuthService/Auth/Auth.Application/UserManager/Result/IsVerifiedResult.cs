using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class IsVerifiedResult : BaseResult
    {
        public bool IsVerified { get; set; }
        public IsVerifiedResult(bool isVerified, Error error = null)
            : base(error)
        {
            IsVerified = isVerified;
        }
    }
}
