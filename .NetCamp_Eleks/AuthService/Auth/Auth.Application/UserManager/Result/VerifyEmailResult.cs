﻿using Auth.Application.Result;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.UserManager.Result
{
    public class VerifyEmailResult : BaseResult
    {
        public string Email { get; set; }
        public VerifyEmailResult(string email, Error error = null)
            :base(error)
        {
            Email = email;
        }
    }
}
