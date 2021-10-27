using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class IsValidResult : BaseResult
    {
        public IsValidResult(Error error = null)
            : base(error)
        {

        }
    }
}
