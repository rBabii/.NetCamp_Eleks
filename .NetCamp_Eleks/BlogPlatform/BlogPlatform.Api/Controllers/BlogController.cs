using BlogPlatform.Application.Managers.BlogManager;
using BlogPlatform.Application.Managers.BlogManager.Params;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using DTOs.BlogPlatform.Models.Request;
using DTOs.BlogPlatform.Models.Response;
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
    public class BlogController : ControllerBase
    {
        private readonly BlogManager _blogManager;
        public BlogController(BlogManager blogManager)
        {
            _blogManager = blogManager;
        }

        [Authorize]
        [Route("api/[controller]/SetupBlog")]
        [HttpPost]
        public IActionResult SetupBlog(SetupBlogRequest setupBlogRequest)
        {
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return StatusCode(500, "Invalid Auth Token.");
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _blogManager.SetupBlog(new SetupBlogParams() 
                    {
                        UserId = UserID,
                        BlogUrl = UserID.ToString(),
                        Title = setupBlogRequest.Title,
                        SubTitle = setupBlogRequest.SubTitle,
                        Visible = setupBlogRequest.Visible
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new DTOs.Common.Models.Error() 
                        { 
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
                    }
                    return Ok(new SetupBlogResponse() 
                    {
                        BlogId = result.Blog.BlogId,
                        BlogUrl = result.Blog.BlogUrl,
                        DateCreated = result.Blog.DateCreated,
                        Visible = result.Blog.Visible,
                        Title = result.Blog.Title,
                        SubTitle = result.Blog.SubTitle
                    });
                }
                return StatusCode(500, "Invalid Auth Token.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Setup Blog failed.");
            }
        }


        [Authorize]
        [Route("api/[controller]/DeleteBlog")]
        [HttpPost]
        public IActionResult DeleteBlog()
        {
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return StatusCode(500, "Invalid Auth Token.");
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _blogManager.DeleteBlog(new DeleteBlogParams()
                    {
                        UserId = UserID
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
                    }
                    return NoContent();
                }
                return StatusCode(500, "Invalid Auth Token.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Delete Blog failed.");
            }
        }
    }
}
