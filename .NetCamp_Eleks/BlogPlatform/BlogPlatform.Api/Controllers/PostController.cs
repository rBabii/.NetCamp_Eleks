using BlogPlatform.Api.Models.Request;
using BlogPlatform.Api.Models.Response;
using BlogPlatform.Application.Managers.PostManager;
using BlogPlatform.Application.Managers.PostManager.Params;
using BlogPlatform.Application.Result;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
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
    public class PostController : ControllerBase
    {
        private readonly PostManager _postManager;
        public PostController(PostManager postManager)
        {
            _postManager = postManager;
        }

        [Authorize]
        [Route("api/[controller]/CreatePost")]
        [HttpPost]
        public IActionResult CreatePost(CreatePostRequest createPostRequest)
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
                    var result = _postManager.CreatePost(new CreatePostParams ()
                    {
                        UserId = UserID,
                        DatePosted = createPostRequest.DatePosted,
                        Visible = createPostRequest.Visible,
                        Title = createPostRequest.Title,
                        SubTitle = createPostRequest.SubTitle,
                        HeaderContent = createPostRequest.HeaderContent,
                        MainContent = createPostRequest.MainContent,
                        FooterContent = createPostRequest.FooterContent
                    });
                    if (!result.IsValid)
                    {
                        return BadRequest(result.Error);
                    }
                    return Ok(new CreatePostResponse()
                    {
                        PostId = result.Post.PostId,
                        BlogId = result.Post.BlogId,
                        DateCreated = result.Post.DateCreated,
                        DatePosted = result.Post.DatePosted,
                        Visible = result.Post.Visible,
                        Title = result.Post.Title,
                        SubTitle = result.Post.SubTitle,
                        HeaderContent = result.Post.HeaderContent,
                        MainContent = result.Post.MainContent,
                        FooterContent = result.Post.FooterContent
                    });
                }
                return StatusCode(500, "Create post failed.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Create post failed.");
            }
        }


        [Authorize]
        [Route("api/[controller]/DeletePost")]
        [HttpPost]
        public IActionResult DeletePost(DeletePostRequest deletePostRequest)
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
                    var result = _postManager.DeletePost( new DeletePostParams() 
                    {
                        UserId = UserID,
                        PostId = deletePostRequest.PostId
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
