﻿using Auth.Application.Options;
using Microsoft.Extensions.Options;

namespace Auth.Application.Helpers.JWT.EmailVerify
{
    public class EmailVerifyTokenHelper : BaseJwtTokenHelper
    {
        public EmailVerifyTokenHelper(IOptions<EmailVerifyTokenOptions> emailVerifyTokenOptions)
            : base(emailVerifyTokenOptions) { }
    }
}
