using Auth.Application.Result;
using Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class LoginResult : BaseResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public LoginResult(string accessToken, string refreshToken, Error error = null)
            : base(error)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
