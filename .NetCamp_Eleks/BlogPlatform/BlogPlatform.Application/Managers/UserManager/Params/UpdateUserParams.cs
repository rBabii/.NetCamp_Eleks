using BlogPlatform.Domain.AgregatesModel.UserAgregate.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.UserManager.Params
{
    public class UpdateUserParams
    {
        [Required(ErrorMessage = "AuthResourceUserId is Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "AuthResourceUserId cant be less then 1")]
        public int AuthResourceUserId { get; set; }  // from Auth Claims

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } // from Auth Claims

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "BirthDate is required.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [Phone(ErrorMessage = "Invalid PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
