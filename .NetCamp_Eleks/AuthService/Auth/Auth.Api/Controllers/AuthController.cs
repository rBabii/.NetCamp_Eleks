using Auth.Api.Models.Request;
using Auth.Api.Models.Response;
using Auth.Application.Exceptions;
using Auth.Application.UserManagment;
using Auth.Domain.UserAggregste;
using Auth.Domain.UserAggregste.Exceptions;
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

                return BadRequest(new ErrorResponse(ErrorMessages));
            }
            try
            {
                var loginResult = _userManager.LogIn(request.Email, request.Password);
                return Ok(new AuthentificatedUserResponse()
                {
                    AccessToken = loginResult.AccessToken,
                    RefreshToken = loginResult.RefreshToken
                });
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(new ErrorResponse("User with current email is not registered."));
            }
            catch (PasswordNotVerifiedException ex)
            {
                return BadRequest(new ErrorResponse("Invalid password."));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Route("api/[controller]/refresh")]
        [HttpPost]
        public IActionResult Refresh(RefreshTokenRequest refreshRequest) 
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new ErrorResponse(ErrorMessages));
            }
            try
            {
                var loginResult = _userManager.RefreshJWT(refreshRequest.RefreshToken);
                return Ok(new AuthentificatedUserResponse()
                {
                    AccessToken = loginResult.AccessToken,
                    RefreshToken = loginResult.RefreshToken
                });
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(new ErrorResponse("Current refresh token not exist."));
            }
            catch (RefreshTokenNotValidException ex)
            {
                return BadRequest(new ErrorResponse("Refresh Token not valid or expired."));
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
                int UserID = 0;
                if (Int32.TryParse(ClaimUserID.Value, out UserID) && UserID != 0)
                {
                    _userManager.LogOut(UserID);
                    return NoContent();
                }
                return BadRequest("LogOut failed.");
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

                return BadRequest(new ErrorResponse(ErrorMessages));
            }
            if (registerRequest.Password != registerRequest.ConfirmPassword)
            {
                return BadRequest(new ErrorResponse("Password not match"));
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
                        Id = Domain.UserAggregste.Enums.Role.User,
                        Description = "Simple User",
                        Name = "User",
                        Permisions = "All"
                    }
                }
                };
                var registerResult = _userManager.Register(user);
                return Ok(new RegisterResponse()
                {
                    Id = registerResult.Id,
                    UserName = registerResult.UserName,
                    Email = registerResult.Email
                });
            }
            catch(UserRegisterException ex)
            {
                return Conflict(new ErrorResponse("User already exist."));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
