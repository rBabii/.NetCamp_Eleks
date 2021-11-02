using BlogPlatform.Application.Managers.UserManager.Resut.Childs;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Resut
{
    public class GetUserResult : BaseResult
    {
        public User User { get; set; }
        public GetUserResult(User user, Error error = null)
            :base (error)
        {
            User = user;
        }
    }
}
