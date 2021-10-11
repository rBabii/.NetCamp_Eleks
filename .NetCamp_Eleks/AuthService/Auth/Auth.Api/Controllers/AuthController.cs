using Auth.Api.Models.Request;
using Auth.Api.Models.Response;
using Auth.Application.Result;
using Auth.Application.UserManagment;
using Auth.Domain.UserAggregate;
using Auth.Domain.UserAggregate.Exceptions;
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

        [Route("api/[controller]/login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            try
            {
                var loginResult = _userManager.LogIn(request.Email, request.Password);
                if (!loginResult.IsValid)
                {
                    return BadRequest(loginResult.Error);
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
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("api/[controller]/login/refresh")]
        [HttpPost]
        public IActionResult Refresh(RefreshTokenRequest refreshRequest) 
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            try
            {
                var loginResult = _userManager.RefreshJWT(refreshRequest.RefreshToken);
                if (!loginResult.IsValid)
                {
                    return BadRequest(loginResult.Error);
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
            catch (Exception)
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
                    return StatusCode(500, "LogOut failed.");
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _userManager.LogOut(UserID);
                    if (!result.IsValid)
                    {
                        return Forbid();
                    }
                    return NoContent();
                }
                return StatusCode(500, "LogOut failed.");
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("/api/[controller]/Register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest(new Error("Password not match"));
            }
            try
            {
                var user = new User()
                {
                    Email = registerRequest.Email,
                    Password = registerRequest.Password,
                    UserName = registerRequest.UserName,
                    Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Id = Domain.UserAggregate.Enums.Role.User,
                            Description = "Simple User",
                            Name = "User",
                            Permisions = "All"
                        }
                    }
                };
                var registerResult = _userManager.Register(user);
                if (!registerResult.IsValid)
                {
                    return BadRequest(registerResult.Error);
                }
                try
                {
                    _userManager.SendEmailVerifyTokenLink(registerResult.User,
                        "http://localhost:5000/api/auth/VerifyUser" + $"?token={registerResult.EmailVerifyToken}");
                }
                catch(Exception ex)
                {
                    //TODO ADD LOGS
                }

                return Ok(new RegisterResponse()
                {
                    Id = registerResult.User.Id,
                    UserName = registerResult.User.UserName,
                    Email = registerResult.User.Email
                });
            }
            catch(UserAddException ex)
            {
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("/api/[controller]/VerifyUser")]
        [HttpGet]
        public IActionResult VerifyUserEmail(string token)
        {
            var result = _userManager.VerifyEmail(token);
            if (!result.IsValid)
            {
                return BadRequest(result.Error);
            }
            return Ok("User verified successfully");
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
                    return StatusCode(500, "User delete failed.");
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _userManager.DeleteUser(UserID);
                    if (!result.IsValid)
                    {
                        return Forbid();
                    }
                    return NoContent();
                }
                return StatusCode(500, "User delete failed.");
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("/api/[controller]/SendResetPasswordToken")]
        [HttpPost]
        public IActionResult SendResetPasswordToken(SendResetPasswordTokenRequest sendResetPasswordTokenRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            var result =_userManager.GenerateResetPasswordToken(sendResetPasswordTokenRequest.Email);
            if (!result.IsValid)
            {
                return BadRequest(result.Error);
            }
            _userManager.SendResetPasswordToken(result.User, result.ResetPasswordToken);
            return Ok("Reset password token sended to your email Address.");
        }

        [Route("/api/[controller]/ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            if(resetPasswordRequest.Password != resetPasswordRequest.ConfirmPassword)
            {
                return BadRequest(new Error("Passwords not match."));
            }
            try
            {
                var result = _userManager.ResetPassword(resetPasswordRequest.Token, resetPasswordRequest.Password);
                if (!result.IsValid)
                {
                    return BadRequest(result.Error);
                }
                return Ok("Password updated.");
            }
            catch (UserUpdateException ex)
            {
                return StatusCode(500);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            
        }
    }
}
