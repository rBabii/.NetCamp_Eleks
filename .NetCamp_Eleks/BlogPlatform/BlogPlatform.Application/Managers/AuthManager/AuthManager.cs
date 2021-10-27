using BlogPlatform.Application.Managers.AuthManager.Params;
using BlogPlatform.Application.Managers.AuthManager.Result;
using BlogPlatform.Infrastructure.HttpServices.Auth;
using External.Result.Base;
using External.DTOs.Auth.Models.Request;
using External.Result.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlogPlatform.Application.Managers.EmailManager;
using BlogPlatform.Application.Managers.EmailManager.Params;
using Microsoft.Extensions.Options;
using External.Options.BlogPlatform;

namespace BlogPlatform.Application.Managers.AuthManager
{
    public class AuthManager
    {
        private readonly AuthService _authService;
        private readonly EmailManager.EmailManager _emailManager;
        private readonly IOptions<FrontAppOptions> _frontAppOptions;

        public AuthManager(AuthService authService, EmailManager.EmailManager emailManager, IOptions<FrontAppOptions> frontAppOptions)
        {
            _authService = authService;
            _emailManager = emailManager;
            _frontAppOptions = frontAppOptions;
        }

        public async Task<RegisterResult> Register(RegisterParams registerParams)
        {
            var validateErrorResult = ParamsValidator.Validate(registerParams);
            if (validateErrorResult != null)
            {
                return new RegisterResult(validateErrorResult);
            }

            var result = await _authService.Register(new RegisterRequest() 
            {
                Email = registerParams.Email,
                Password = registerParams.Password,
                ConfirmPassword = registerParams.ConfirmPassword
            });

            if (!result.IsSuccess && result.Error.ErrorType != External.DTOs.Common.Enums.ErrorType.Info)
            {
                return new RegisterResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }

            var emailResult = await _emailManager.Send(new EmailManager.Params.SendEmailParams() 
            {
                EmailAddressesFrom = new List<EmailAddress>() 
                {
                    new EmailAddress()
                    {
                        Name = "BlogPlat-form",
                        Address = "noreply@BlogPlat-Form.com"
                    }
                },
                EmailAddressesTo = new List<EmailAddress>()
                {
                    new EmailAddress()
                    {
                        Name = "NewUser",
                        Address = result.ResponseObject.Email
                    }
                },
                Subject = "Verfy Email",
                Body = string.Format("<a href='{0}'>Verify Email</a>", _frontAppOptions.Value.VerifyUserUrl + "?token=" + result.ResponseObject.EmailVerifyToken)
            });

            //SEND EMAIL WITH VERIFY LINK (url?token={resetPasswordToken})

            return new RegisterResult();
        }

        public async Task<VerifyEmailResult> VerifyEmail(VerifyEmailParams verifyEmailParams)
        {
            var validateErrorResult = ParamsValidator.Validate(verifyEmailParams);
            if (validateErrorResult != null)
            {
                return new VerifyEmailResult(validateErrorResult);
            }

            var result = await _authService.VerifyUserEmail(new VerifyUserEmailRequest()
            {
                Token = verifyEmailParams.Token
            });

            if (!result.IsSuccess && result.Error.ErrorType != External.DTOs.Common.Enums.ErrorType.Info)
            {
                return new VerifyEmailResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }

            return new VerifyEmailResult();
        }

        public async Task<SendResetPasswordLinkResult> SendResetPasswordLink(SendResetPasswordLinkParams sendResetPasswordLinkParams)
        {
            var validateErrorResult = ParamsValidator.Validate(sendResetPasswordLinkParams);
            if (validateErrorResult != null)
            {
                return new SendResetPasswordLinkResult(validateErrorResult);
            }

            var result = await _authService.GetResetPasswordToken(new GetResetPasswordTokenRequest()
            {
                Email = sendResetPasswordLinkParams.Email
            });

            if (!result.IsSuccess && result.Error.ErrorType != External.DTOs.Common.Enums.ErrorType.Info)
            {
                return new SendResetPasswordLinkResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }

            //Send EMAIL WITH RESET PASSWORD LINK (url?token={resetPasswordToken})

            return new SendResetPasswordLinkResult();
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordParams resetPasswordParams)
        {
            var validateErrorResult = ParamsValidator.Validate(resetPasswordParams);
            if (validateErrorResult != null)
            {
                return new ResetPasswordResult(validateErrorResult);
            }

            var result = await _authService.ResetPassword(new ResetPasswordRequest()
            {
                Token = resetPasswordParams.Token,
                Password = resetPasswordParams.Password,
                ConfirmPassword = resetPasswordParams.ConfirmPassword
            });

            if (!result.IsSuccess && result.Error.ErrorType != External.DTOs.Common.Enums.ErrorType.Info)
            {
                return new ResetPasswordResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }

            return new ResetPasswordResult();
        }
    }
}
