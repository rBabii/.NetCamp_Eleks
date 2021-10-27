using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Resut
{
    public class IsUserSetupedResult : BaseResult
    {
        public bool IsUserSetuped { get; set; }
        public IsUserSetupedResult(bool isUserSetuped, Error error = null)
            :base(error)
        {
            IsUserSetuped = isUserSetuped;
        }
    }
}
