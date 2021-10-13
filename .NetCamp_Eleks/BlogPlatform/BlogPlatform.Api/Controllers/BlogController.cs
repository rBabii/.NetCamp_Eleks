using BlogPlatform.Api.Models.Request;
using BlogPlatform.Api.Models.Response;
using BlogPlatform.Application.Managers.BlogManager;
using BlogPlatform.Application.Managers.BlogManager.Params;
using BlogPlatform.Application.Result;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
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
        public readonly BlogManager _blogManager;
        public BlogController(BlogManager blogManager)
        {
            _blogManager = blogManager;
        }

        [Authorize]
        [Route("api/[controller]/SetupBlog")]
        [HttpPost]
        public IActionResult SetupBlog(SetupBlogRequest setupBlogRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                var ClaimUserName = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.UniqueName);
                if (ClaimUserID == null || ClaimUserName == null)
                {
                    return StatusCode(500);
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _blogManager.SetupBlog(new SetupBlogParams() 
                    {
                        UserId = UserID,
                        BlogUrl = ClaimUserName.Value,
                        Title = setupBlogRequest.Title,
                        SubTitle = setupBlogRequest.SubTitle,
                        Visible = setupBlogRequest.Visible
                    });
                    if (!result.IsValid)
                    {
                        return BadRequest(result.Error);
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
                return StatusCode(500, "Setup Blog failed.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Setup Blog failed.");
            }
        }


        [Authorize]
        [Route("api/[controller]/DeleteBlog")]
        [HttpPost]
        public IActionResult DeleteBlog()
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> ErrorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                return BadRequest(new Error(ErrorMessages));
            }
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return StatusCode(500);
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _blogManager.DeleteBlog(new DeleteBlogParams()
                    {
                        UserId = UserID
                    });
                    if (!result.IsValid)
                    {
                        return BadRequest(result.Error);
                    }
                    return NoContent();
                }
                return StatusCode(500, "Delete Blog failed.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Delete Blog failed.");
            }
        }
    }
}
