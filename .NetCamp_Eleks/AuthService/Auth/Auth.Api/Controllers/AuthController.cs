using Auth.Application.UserManager;
using Auth.Application.UserManager.Params;
using Auth.Domain.UserAggregate;
using Auth.Domain.UserAggregate.Exceptions;
using External.DTOs.Auth.Models.Request;
using External.DTOs.Auth.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager _userManager;

        public AuthController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [Route("api/[controller]/IsVerified")]
        [HttpPost]
        public IActionResult IsVerified()
        {
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return Forbid();
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _userManager.IsVerified(new IsVerifiedParams()
                    {
                        UserId = UserID
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new External.DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
                    }
                    return Ok(new IsVerifiedResponse()
                    {
                        IsVerified = result.IsVerified
                    });
                }
                return Forbid();
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("api/[controller]/IsValid")]
        [HttpPost]
        public IActionResult IsValid(IsValidRequest request)
        {
            try
            {
                var loginResult = _userManager.IsValid(new IsValidParams()
                {
                    UserEmail = request.UserEmail,
                    UserId = request.UserId
                });
                if (!loginResult.IsValid && !loginResult.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error()
                    {
                        ErrorMessages = loginResult.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)loginResult.Error.ErrorType
                    });
                }
                return Ok();
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("api/[controller]/login")]
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var loginResult = _userManager.Login(new LoginParams() 
                {
                    Email = request.Email,
                    Password = request.Password
                });
                if (!loginResult.IsValid && !loginResult.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error() 
                    {
                        ErrorMessages = loginResult.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)loginResult.Error.ErrorType
                    });
                }
                return Ok(new AuthentificatedUserResponse()
                {
                    AccessToken = loginResult.AccessToken,
                    RefreshToken = loginResult.RefreshToken
                });
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("api/[controller]/login/refresh")]
        [HttpPost]
        public IActionResult Refresh(RefreshTokenRequest refreshRequest) 
        {
            try
            {
                var loginResult = _userManager.LoginRefresh(new LoginRefreshParams() 
                {
                    RefreshToken = refreshRequest.RefreshToken
                });
                if (!loginResult.IsValid && !loginResult.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error()
                    {
                        ErrorMessages = loginResult.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)loginResult.Error.ErrorType
                    });
                }
                return Ok(new AuthentificatedUserResponse()
                {
                    AccessToken = loginResult.AccessToken,
                    RefreshToken = loginResult.RefreshToken
                });
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("api/[controller]/logout")]
        [HttpPost]
        public IActionResult LogOut()
        {
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return Forbid();
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _userManager.Logout(new LogoutParams() 
                    {
                        UserId = UserID
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new External.DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
                    }
                    return NoContent();
                }
                return Forbid();
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("/api/[controller]/Register")]
        [HttpPost]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            try
            {
                var registerResult = _userManager.Register(new RegisterParams() 
                {
                    Email = registerRequest.Email,
                    Password = registerRequest.Password,
                    ConfirmPassword = registerRequest.ConfirmPassword
                });
                if (!registerResult.IsValid && !registerResult.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error()
                    {
                        ErrorMessages = registerResult.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)registerResult.Error.ErrorType
                    });
                }

                return Ok(new RegisterResponse()
                {
                    Id = registerResult.User.Id,
                    Email = registerResult.User.Email,
                    EmailVerifyToken = registerResult.EmailVerifyToken
                });
            }
            catch(UserAddException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("/api/[controller]/VerifyUser")]
        [HttpPost]
        public IActionResult VerifyUserEmail(VerifyUserEmailRequest verifyUserEmailRequest)
        {
            try
            {
                var result = _userManager.VerifyEmail(new VerifyEmailParams()
                {
                    Token = verifyUserEmailRequest.Token
                });
                if (!result.IsValid && !result.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error()
                    {
                        ErrorMessages = result.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                    });
                }
                return Ok("User verified successfully");
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("/api/[controller]/Delete")]
        [HttpPost]
        public IActionResult Delete()
        {
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return Forbid();
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _userManager.DeleteUser(new DeleteUserParams() 
                    {
                        UserId = UserID
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new External.DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
                    }
                    return NoContent();
                }
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Route("/api/[controller]/GetResetPasswordToken")]
        [HttpPost]
        public IActionResult GetResetPasswordToken(GetResetPasswordTokenRequest getResetPasswordTokenRequest)
        {
            var result =_userManager.GenerateResetPasswordToken(new GenerateResetPasswordTokenParams() 
            {
                Email = getResetPasswordTokenRequest.Email
            });
            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                });
            }
            return Ok(new GetResetPasswordTokenResponse() 
            {
                Token = result.ResetPasswordToken
            });
        }

        [Route("/api/[controller]/ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            try
            {
                var result = _userManager.ResetPassword(new ResetPasswordParams() 
                {
                    Token = resetPasswordRequest.Token,
                    Password = resetPasswordRequest.Password,
                    ConfirmPassword = resetPasswordRequest.ConfirmPassword
                });
                if (!result.IsValid && !result.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error()
                    {
                        ErrorMessages = result.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                    });
                }
                return Ok("Password updated.");
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            
        }
    }
}
