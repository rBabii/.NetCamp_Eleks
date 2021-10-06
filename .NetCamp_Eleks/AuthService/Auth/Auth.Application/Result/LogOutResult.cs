using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public class LogOutResult : BaseResult
    {
        public bool IsSucceed { get; set; }
        public LogOutResult(bool isSucceed, Error error = null)
            : base(error)
        {
            IsSucceed = isSucceed;
        }
    }
}
