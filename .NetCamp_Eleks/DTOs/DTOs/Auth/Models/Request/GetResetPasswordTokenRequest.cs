using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.Auth.Models.Request
{
    public class GetResetPasswordTokenRequest
    {
        public string Email { get; set; }
    }
}
