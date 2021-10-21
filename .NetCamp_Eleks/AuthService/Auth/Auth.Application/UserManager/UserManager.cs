﻿using Auth.Application.Helpers;
using Auth.Application.Helpers.JWT.Auth;
using Auth.Application.Helpers.JWT.EmailVerify;
using Auth.Application.Helpers.JWT.RefreshToken;
using Auth.Application.Helpers.JWT.ResetPassword;
using Auth.Application.Result;
using Auth.Application.UserManager.Params;
using Auth.Application.UserManager.Result;
using Auth.Domain.UserAggregate;
using Auth.Infrastructure.Services.Email;
using Auth.Infrastructure.Services.Email.Models;
using Result.Base;
using Result.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Auth.Application.UserManager
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly RefreshTokenHelper _refreshTokenHelper;
        private readonly AuthTokenHelper _authTokenHelper;
        private readonly ResetPasswordTokenHelper _resetPasswordTokenHelper;
        private readonly EmailVerifyTokenHelper _emailVerifyTokenHelper;
        private readonly EmailService _emailService;

        public UserManager(IUserRepository userRepository,
                            PasswordHasher passwordHasher,
                            RefreshTokenHelper refreshTokenHelper,
                            AuthTokenHelper authTokenHelper,
                            ResetPasswordTokenHelper resetPasswordTokenHelper,
                            EmailVerifyTokenHelper emailVerifyTokenHelper,
                            EmailService emailService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenHelper = refreshTokenHelper;
            _authTokenHelper = authTokenHelper;
            _resetPasswordTokenHelper = resetPasswordTokenHelper;
            _emailVerifyTokenHelper = emailVerifyTokenHelper;
            _emailService = emailService;
        }

        private AuthentificateResult Authentificate(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if(user == null)
            {
                return new AuthentificateResult(user, new Error($"User with Email: {email} does not exist", ErrorType.Validation));
            }
            var check = _passwordHasher.Check(user.Password, password);

            if (!check.IsVerified)
            {
                return new AuthentificateResult(user, new Error("Invalid Password", ErrorType.Validation));
            }
            return new AuthentificateResult(user);
        }
        public LoginResult Login(LoginParams loginParams)
        {
            var validateErrorResult = ParamsValidator.Validate(loginParams);
            if(validateErrorResult != null)
            {
                return new LoginResult("", "", validateErrorResult);
            }

            var authResult = Authentificate(loginParams.Email, loginParams.Password);
            if (!authResult.IsValid)
            {
                return new LoginResult("", "", authResult.Error);
            }
            var token = _authTokenHelper.GenerateJWT(authResult.User);
            var refreshToken = _refreshTokenHelper.GenerateJWT(authResult.User);
            authResult.User.RefreshToken = refreshToken;
            _userRepository.AddOrUpdate(authResult.User);

            return new LoginResult(token, refreshToken);
        }

        public LogoutResult Logout(LogoutParams logoutParams)
        {
            var validateErrorResult = ParamsValidator.Validate(logoutParams);
            if (validateErrorResult != null)
            {
                return new LogoutResult(validateErrorResult);
            }

            var user = _userRepository.Get(logoutParams.UserId);
            if(user == null)
            {
                return new LogoutResult(new Error("User does not exist to logout.", ErrorType.Validation));
            }
            user.RefreshToken = null;
            _userRepository.AddOrUpdate(user);
            return new LogoutResult();
        }

        public LoginResult LoginRefresh(LoginRefreshParams loginRefreshParams)
        {
            var validateErrorResult = ParamsValidator.Validate(loginRefreshParams);
            if (validateErrorResult != null)
            {
                return new LoginResult("", "", validateErrorResult);
            }

            var user = _userRepository.GetByRefreshToken(loginRefreshParams.RefreshToken);
            if (user == null)
            {
                return new LoginResult("", "", new Error("Refresh token does not exist.", ErrorType.Validation));
            }
            var result = _refreshTokenHelper.Validate(loginRefreshParams.RefreshToken);
            if (result == null)
            {
                return new LoginResult("", "", new Error("Invalid refresh token.", ErrorType.Validation));
            }
            var token = _authTokenHelper.GenerateJWT(user);
            var newRefreshToken = _refreshTokenHelper.GenerateJWT(user);
            user.RefreshToken = newRefreshToken;
            _userRepository.AddOrUpdate(user);
            return new LoginResult(token, newRefreshToken);
        }

        public RegisterResult Register(RegisterParams registerParams)
        {
            var validateErrorResult = ParamsValidator.Validate(registerParams);
            if (validateErrorResult != null)
            {
                return new RegisterResult(null, "", validateErrorResult);
            }

            var serchedUser = _userRepository.GetByEmail(registerParams.Email);
            
            if(serchedUser != null)
            {
                return new RegisterResult(null, "", new Error($"User with Email: {registerParams.Email} allready exist.", ErrorType.Validation));
            }

            var user = new User()
            {
                Email = registerParams.Email,
                Password = registerParams.Password,
                Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Id = Domain.UserAggregate.Enums.Role.User,
                            Description = Domain.UserAggregate.Enums.Role.User.ToString() + " Role",
                            Name = Domain.UserAggregate.Enums.Role.User.ToString()
                        }
                    }
            };

            user.Password = _passwordHasher.Hash(user.Password);
            var registeredUser = _userRepository.AddOrUpdate(user);

            var emailVerifyTokenResult = GenerateEmailVerificationToken(user);

            if (!emailVerifyTokenResult.IsValid)
            {
                return new RegisterResult(registeredUser, "", emailVerifyTokenResult.Error);
            }
                
            return new RegisterResult(registeredUser, emailVerifyTokenResult.EmailVerificationToken);
        }

        //private void SendVerifyTokenLinkEmail(User user, string Link)
        //{
        //    var email = new SendEmailModel()
        //    {
        //        EmailAddressesTo = new List<EmailAddress>()
        //        {
        //            new EmailAddress()
        //            {
        //                Address = user.Email,
        //                Name = ""
        //            }
        //        },
        //        EmailAddressesFrom = new List<EmailAddress>()
        //        {
        //            new EmailAddress()
        //            {
        //                Address = "auth@auth.com",
        //                Name = "Auth"
        //            }
        //        },
        //        Subject = "Confirm Email Link",
        //        Body = $"{Link}"
        //    };
        //    _emailService.SendEmail(email);
        //}

        //private void SendResetPasswordTokenEmail(User user, string token)
        //{
        //    var email = new SendEmailModel()
        //    {
        //        EmailAddressesTo = new List<EmailAddress>()
        //        {
        //            new EmailAddress()
        //            {
        //                Address = user.Email,
        //                Name = ""
        //            }
        //        },
        //        EmailAddressesFrom = new List<EmailAddress>()
        //        {
        //            new EmailAddress()
        //            {
        //                Address = "auth@auth.com",
        //                Name = "Auth"
        //            }
        //        },
        //        Subject = "Reset Password Token",
        //        Body = $"{token}"
        //    };
        //    _emailService.SendEmail(email);
        //}

        public DeleteUserResult DeleteUser(DeleteUserParams deleteUserParams)
        {
            var validateErrorResult = ParamsValidator.Validate(deleteUserParams);
            if (validateErrorResult != null)
            {
                return new DeleteUserResult(validateErrorResult);
            }

            var user = _userRepository.Get(deleteUserParams.UserId);

            if (user == null)
            {
                return new DeleteUserResult(new Error("User is not found to delete.", ErrorType.Validation));
            }
            var deleteResult = _userRepository.Delete(user);
            if (!deleteResult)
            {
                return new DeleteUserResult(new Error("user is not exist or was deleted before.", ErrorType.SystemError));
            }
            return new DeleteUserResult();
        }

        public GenerateResetPasswordTokenResult GenerateResetPasswordToken(GenerateResetPasswordTokenParams generateResetPasswordTokenParams)
        {
            var validateErrorResult = ParamsValidator.Validate(generateResetPasswordTokenParams);
            if (validateErrorResult != null)
            {
                return new GenerateResetPasswordTokenResult("", null, validateErrorResult);
            }
            var user = _userRepository.GetByEmail(generateResetPasswordTokenParams.Email);
            if(user == null)
            {
                return new GenerateResetPasswordTokenResult("", user, new Error($"User with Email: {generateResetPasswordTokenParams.Email} is not exist.", ErrorType.Validation));
            }
            var token = _resetPasswordTokenHelper.GenerateJWT(user);
            if (string.IsNullOrEmpty(token))
            {
                return new GenerateResetPasswordTokenResult("", user, new Error("reset password token generating failed.", ErrorType.SystemError));
            }
            return new GenerateResetPasswordTokenResult(token, user);
        }

        private GenerateEmailVerificationTokenResult GenerateEmailVerificationToken(User user)
        {
            if (user == null || user.Id < 1)
            {
                return new GenerateEmailVerificationTokenResult("", new Error("User is not valid - email verification token is not generated.", ErrorType.Info));
            }
            var token = _emailVerifyTokenHelper.GenerateJWT(user);
            return new GenerateEmailVerificationTokenResult(token);
        }

        public ResetPasswordResult ResetPassword(ResetPasswordParams resetPasswordParams)
        {
            var validateErrorResult = ParamsValidator.Validate(resetPasswordParams);
            if (validateErrorResult != null)
            {
                return new ResetPasswordResult(validateErrorResult);
            }

            var result = _resetPasswordTokenHelper.Validate(resetPasswordParams.Token);
            if (result == null)
            {
                return new ResetPasswordResult(new Error("Invalid token.", ErrorType.Validation));
            }

            var UserIdClaim = result.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if(UserIdClaim == null)
            {
                return new ResetPasswordResult(new Error("Invalid token", ErrorType.Validation));
            }
            if (Int32.TryParse(UserIdClaim.Value, out int userId) && userId != 0)
            {
                var user = _userRepository.Get(userId);
                if (user == null)
                {
                    return new ResetPasswordResult(new Error("User does not exist or deleted.", ErrorType.Validation));
                }
                user.Password = _passwordHasher.Hash(resetPasswordParams.Password);
                _userRepository.AddOrUpdate(user);
                return new ResetPasswordResult();
            }
            return new ResetPasswordResult(new Error("Invalid token.", ErrorType.Validation));
        }

        public VerifyEmailResult VerifyEmail(VerifyEmailParams verifyEmailParams)
        {
            var validateErrorResult = ParamsValidator.Validate(verifyEmailParams);
            if (validateErrorResult != null)
            {
                return new VerifyEmailResult(validateErrorResult);
            }

            var result = _emailVerifyTokenHelper.Validate(verifyEmailParams.Token);
            if(result == null)
            {
                return new VerifyEmailResult(new Error("Invalid token.", ErrorType.Validation));
            }
            var UserIdClaim = result.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
            if (UserIdClaim == null)
            {
                return new VerifyEmailResult(new Error("Invalid token.", ErrorType.Validation));
            }
            if (Int32.TryParse(UserIdClaim.Value, out int userId) && userId != 0)
            {
                var user = _userRepository.Get(userId);
                if(user == null)
                {
                    return new VerifyEmailResult(new Error("User does not exist or deleted.", ErrorType.Validation));
                }
                if (user.IsVerified)
                {
                    return new VerifyEmailResult(new Error("User allready Verified.", ErrorType.Validation));
                }
                 user.IsVerified = true;
                 _userRepository.AddOrUpdate(user);
                return new VerifyEmailResult();
            }
            return new VerifyEmailResult(new Error("Invalid token.", ErrorType.Validation));
        }
    }
}