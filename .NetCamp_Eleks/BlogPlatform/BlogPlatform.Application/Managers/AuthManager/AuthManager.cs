using BlogPlatform.Application.Managers.AuthManager.Params;
using BlogPlatform.Application.Managers.AuthManager.Result;
using BlogPlatform.Infrastructure.HttpServices.Auth;
using Result.Base;
using DTOs.Auth.Models.Request;
using Result.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.AuthManager
{
    public class AuthManager
    {
        private readonly AuthService _authService;

        public AuthManager(AuthService authService)
        {
            _authService = authService;
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

            if (!result.IsSuccess && result.Error.ErrorType != DTOs.Common.Enums.ErrorType.Info)
            {
                return new RegisterResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }

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

            if (!result.IsSuccess && result.Error.ErrorType != DTOs.Common.Enums.ErrorType.Info)
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

            if (!result.IsSuccess && result.Error.ErrorType != DTOs.Common.Enums.ErrorType.Info)
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

            if (!result.IsSuccess && result.Error.ErrorType != DTOs.Common.Enums.ErrorType.Info)
            {
                return new ResetPasswordResult(new Error(result.Error.ErrorMessages, (ErrorType)result.Error.ErrorType));
            }

            return new ResetPasswordResult();
        }
    }
}
