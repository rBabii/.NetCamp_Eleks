using BlogPlatform.Application.Managers.UserManager;
using External.DTOs.BlogPlatform.Models.Request;
using External.DTOs.BlogPlatform.Models.Request.Enums;
using External.DTOs.BlogPlatform.Models.Response;
using External.DTOs.Common.Enums;
using External.DTOs.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;
        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [Route("api/[controller]/Get")]
        [HttpGet]
        public IActionResult Get()
        {
            var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);

            if (ClaimUserID == null)
            {
                return StatusCode(500, "Invalid Auth Token.");
            }
            if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
            {
                var result = _userManager.GetUser(new Application.Managers.UserManager.Params.GetUserParams()
                {
                    AuthResourceUserId = UserID
                });

                if (!result.IsValid && !result.CanContinue)
                {
                    return BadRequest(new Error()
                    {
                        ErrorMessages = result.Error.ErrorMessages,
                        ErrorType = (ErrorType)result.Error.ErrorType
                    });
                }

                return Ok(new GetUserResponse()
                {
                    Email = result.User.Email,
                    BirthDate = result.User.BirthDate,
                    FirstName = result.User.FirstName,
                    Gender = (Gender)result.User.Gender,
                    LastName = result.User.LastName,
                    PhoneNumber = result.User.PhoneNumber,
                    BlogUrl = result.User.Blog == null ? "" : result.User.Blog.BlogUrl,
                    ImageUrl = result.User.ImageUrl,
                    IsVerified = result.User.IsVerified
                });
            }

            return StatusCode(500, "Invalid Auth Token.");
        }

        [Authorize]
        [Route("api/[controller]/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserRequest updateUserRequest)
        {
            var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
            var ClaimUserEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Email);
            if (ClaimUserID == null || ClaimUserEmail == null)
            {
                return StatusCode(500, "Invalid Auth Token.");
            }

            if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
            {
                var result = await _userManager.UpdateUser(new Application.Managers.UserManager.Params.UpdateUserParams()
                {
                    AuthResourceUserId = UserID,
                    Email = ClaimUserEmail.Value,
                    FirstName = updateUserRequest.FirstName,
                    LastName = updateUserRequest.LastName,
                    Gender = (Domain.AgregatesModel.UserAgregate.Enums.Gender)updateUserRequest.Gender,
                    BirthDate = updateUserRequest.BirthDate,
                    PhoneNumber = updateUserRequest.PhoneNumber
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
            return StatusCode(500, "Invalid Auth Token.");
        }

        [Authorize]
        [Route("api/[controller]/IsUserSetuped")]
        [HttpPost]
        public IActionResult IsUserSetuped()
        {
            var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);

            if (ClaimUserID == null)
            {
                return StatusCode(500, "Invalid Auth Token.");
            }

            if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
            {
                var result = _userManager.IsUserSetuped(new Application.Managers.UserManager.Params.IsUserSetupedParams()
                {
                    AuthResourceUserId = UserID
                });

                if (!result.IsValid && !result.CanContinue)
                {
                    return BadRequest(new Error()
                    {
                        ErrorMessages = result.Error.ErrorMessages,
                        ErrorType = (ErrorType)result.Error.ErrorType
                    });
                }

                return Ok(new External.DTOs.BlogPlatform.Models.Response.IsUserSetupedResponse()
                {
                    IsUserSetuped = result.IsUserSetuped
                });
            }

            return StatusCode(500, "Invalid Auth Token.");
        }

        [Route("api/[controller]/SaveUserImage")]
        [HttpPost]
        public IActionResult SaveUserImage(SaveUserImageRequest saveUserImageRequest)
        {
            var result = _userManager.SaveUserImage(new Application.Managers.UserManager.Params.SaveUserImageParams() 
            {
                AuthResourceUserId = saveUserImageRequest.AuthResourceUserId,
                Email = saveUserImageRequest.Email,
                ImageName = saveUserImageRequest.ImageName
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
