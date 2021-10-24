using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Application.Managers.EmailManager.Params
{
    public class EmailAddress
    {
        [Required(ErrorMessage = "Name is Required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Address is Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Address { get; set; }
    }
}
