using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.Auth.Models.Request
{
    public class GetEmailVerificationTokenRequest
    {
        public string Email { get; set; }
    }
}
