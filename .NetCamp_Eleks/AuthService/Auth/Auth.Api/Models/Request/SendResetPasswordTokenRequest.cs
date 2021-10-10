using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Models.Request
{
    public class SendResetPasswordTokenRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
