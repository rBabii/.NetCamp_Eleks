using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Params
{
    public class LoginRefreshParams
    {
        [Required(ErrorMessage = "RefreshToken is Required.")]
        public string RefreshToken { get; set; }
    }
}
