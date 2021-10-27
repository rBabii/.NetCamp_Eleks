using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Resut
{
    public class UpdateUserResult : BaseResult
    {
        public UpdateUserResult(Error error = null)
            : base(error)
        {

        }
    }
}
