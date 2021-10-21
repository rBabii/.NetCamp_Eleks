using Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public class CheckPasswordResult : BaseResult
    {
        public bool IsVerified { get; set; }
        public bool NeedsUpgrade { get; set; }
        public CheckPasswordResult(bool isVerified, bool needsUpgrade, Error error = null)
            : base(error)
        {
            IsVerified = isVerified;
            NeedsUpgrade = needsUpgrade;
        }
    }
}
