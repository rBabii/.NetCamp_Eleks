using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.Auth.Models.Request
{
    public class VerifyUserEmailRequest
    {
        public string Token { get; set; }
    }
}
