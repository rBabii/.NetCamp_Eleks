using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.AuthManager.Params
{
    public class VerifyEmailParams
    {
        [Required(ErrorMessage = "Token is Required.")]
        public string Token { get; set; }
    }
}
