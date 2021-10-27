using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.Auth.Models.Request
{
    public class IsValidRequest
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
