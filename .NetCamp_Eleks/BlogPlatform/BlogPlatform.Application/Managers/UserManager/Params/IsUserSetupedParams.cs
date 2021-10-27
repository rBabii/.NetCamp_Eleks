using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Params
{
    public class IsUserSetupedParams
    {
        [Required(ErrorMessage = "AuthResourceUserId is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "AuthResourceUserId cant be less then 1")]
        public int AuthResourceUserId { get; set; }  // from Auth Claims
    }
}
