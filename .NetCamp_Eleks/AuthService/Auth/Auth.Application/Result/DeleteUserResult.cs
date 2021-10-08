using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Result
{
    public class DeleteUserResult : BaseResult
    {
        public DeleteUserResult(Error error = null)
            : base(error)
        {

        }
    }
}
