using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Params
{
    public class DeleteUserParams
    {
        [Required(ErrorMessage = "UserId is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId cant be less then 1")]
        public int UserId { get; set; }
    }
}
