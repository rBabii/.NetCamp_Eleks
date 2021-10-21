using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Params
{
    public class ResetPasswordParams
    {
        [Required(ErrorMessage = "Password is Required.")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "Invalid Password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
            ErrorMessage = "Invalid Confirm Password")]
        [Compare("Password",
            ErrorMessage = "Password and Confirm Password is not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Token is Required.")]
        public string Token { get; set; }
    }
}
