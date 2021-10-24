using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace External.DTOs.Auth.Models.Response
{
    public class RegisterResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string EmailVerifyToken { get; set; }
    }
}
