using AttachmentService.Application.Managers.FileManger;
using External.DTOs.AttachmentService.Models.Request;
using External.DTOs.AttachmentService.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AttachmentService.Api.Controllers
{
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly FileManager _fileManager;
        public AttachmentController(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [Route("api/[controller]/SaveSingleImage")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult SaveSingleImage([FromForm] SaveSingleImageRequest saveSingleImageRequest)
        {
            try
            {
                var result = _fileManager.SaveSingleImage(new Application.Managers.FileManger.Params.SaveSingleImageParams()
                {
                    Image = saveSingleImageRequest.Image,
                    Key = saveSingleImageRequest.Key
                });

                if (!result.IsValid && !result.CanContinue)
                {
                    return BadRequest(new External.DTOs.Common.Models.Error()
                    {
                        ErrorMessages = result.Error.ErrorMessages,
                        ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                    });
                }
                return Ok(new SaveSingleImageResponse() 
                {
                    Key = result.Key,
                    FileName = result.FileName,
                    Url = result.Url
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [Route("api/[controller]/SaveUserImage")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> SaveUserImage([FromForm] SaveUserImageRequest saveUserImageRequest)
        {
            try
            {

                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                var ClaimUserEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Email);
                if (ClaimUserID == null || ClaimUserEmail == null)
                {
                    return StatusCode(500, "Invalid Auth Token.");
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = await _fileManager.SaveUserImage(new Application.Managers.FileManger.Params.SaveUserImageParams() 
                    {
                        AuthResourceUserId = UserID,
                        Email = ClaimUserEmail.Value,
                        Image = saveUserImageRequest.Image
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new External.DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
                    }
                    return Ok();
                }
                return StatusCode(500, "Invalid Auth Token.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
