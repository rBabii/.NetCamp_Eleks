using External.DTOs.BlogPlatform.Models.Request.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Request
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }
    }
}
