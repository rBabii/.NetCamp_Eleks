using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace External.DTOs.Auth.Models.Request
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
