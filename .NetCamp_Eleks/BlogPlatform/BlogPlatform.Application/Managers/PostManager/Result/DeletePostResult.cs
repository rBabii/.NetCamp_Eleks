using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Result
{
    public class DeletePostResult : BaseResult
    {
        public DeletePostResult(Error error = null)
            :base(error)
        {   
        }
    }
}
