using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace External.DTOs.Auth.Models.Response
{
    public class AuthentificatedUserResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
