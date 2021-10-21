using DTOs.Auth.Models.Response;
using Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.AuthManager.Result
{
    public class RegisterResult : BaseResult
    {
        public RegisterResult(Error error = null)
            : base(error)
        {

        }
    }
}
