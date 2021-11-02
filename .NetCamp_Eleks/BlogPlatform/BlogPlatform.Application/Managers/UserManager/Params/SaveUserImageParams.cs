using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Params
{
    public class SaveUserImageParams
    {
        [Required(ErrorMessage = "AuthResourceUserId is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "AuthResourceUserId cant be less then 1")]
        public int AuthResourceUserId { get; set; }  // from Auth Claims

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } // from Auth Claims

        public string ImageName { get; set; }
    }
}
