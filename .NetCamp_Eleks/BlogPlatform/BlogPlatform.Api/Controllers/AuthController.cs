﻿using BlogPlatform.Application.Managers.AuthManager;
using BlogPlatform.Application.Managers.AuthManager.Params;
using External.DTOs.Auth.Models.Request;
using External.DTOs.Common.Enums;
using External.DTOs.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthManager _authManager;
        public AuthController(AuthManager authManager)
        {
            _authManager = authManager;
        }

        [Route("api/[controller]/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await _authManager.Register(new RegisterParams()
            {
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                ConfirmPassword = registerRequest.ConfirmPassword
            });

            if(!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new Error() 
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (ErrorType)result.Error.ErrorType
                });
            }

            return Ok();
        }

        [Route("api/[controller]/Verify")]
        [HttpPost]
        public async Task<IActionResult> Verify(VerifyUserEmailRequest verifyUserEmailRequest)
        {
            var result = await _authManager.VerifyEmail(new VerifyEmailParams() 
            {
                Token = verifyUserEmailRequest.Token
            });

            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (ErrorType)result.Error.ErrorType
                });
            }

            return Ok();
        }
    }
}