using BlogPlatform.Application.Managers.BlogManager;
using BlogPlatform.Application.Managers.BlogManager.Params;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using External.DTOs.BlogPlatform.Models.Request;
using External.DTOs.BlogPlatform.Models.Response;
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

        [Route("api/[controller]/GetSingleBlogPage/{blogUrl}")]
        [HttpGet]
        public IActionResult GetSingleBlogPage(string blogUrl)
        {
            var result = _blogManager.GetSingleBlogPage(new GetSingleBlogPageParams() 
            {
                BlogUrl = blogUrl
            });
            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                });
            }
            return Ok(new GetSingleBlogPageResponse()
            {
                Posts = result.Blog.Posts == null ? null : result.Blog.Posts
                .Select(p => new External.DTOs.BlogPlatform.Models.Response.Childs.GetSingleBlogPagePostItem() 
                {
                    DatePosted = p.DatePosted,
                    SubTitle = p.SubTitle,
                    FooterContent = p.FooterContent,
                    DateCreated = p.DateCreated,
                    HeaderContent = p.HeaderContent,
                    MainContent= p.MainContent,
                    Title = p.Title
                }).ToList(),
                SubTitle = result.Blog.SubTitle,
                Title = result.Blog.Title,
                DateCreated = result.Blog.DateCreated,
                AuthorFirstName = result.Blog.User.FirstName,
                AuthorLastName = result.Blog.User.LastName
            });
        }

        [Route("api/[controller]/GetBlogList")]
        [HttpGet]
        public IActionResult GetBlogList()
        {
            var result = _blogManager.GetBlogList();
            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                });
            }
            return Ok(new GetBlogListResponse()
            {
                Blogs = result.Blogs != null && result.Blogs.Count > 0 ? 
                result.Blogs
                .Select(b => new External.DTOs.BlogPlatform.Models.Response.Childs.GetBlogListItem() 
                {
                    DateCreated = b.DateCreated,
                    BlogUrl = b.BlogUrl,
                    Title = b.Title,
                    SubTitle = b.SubTitle,
                    AuthorFirstName = b.User.FirstName,
                    AuthorLastName = b.User.LastName
                }).ToList() : null,
                Error = result.Error != null ? new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                } : null
            });
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
                        BlogUrl = setupBlogRequest.BlogUrl,
                        Title = setupBlogRequest.Title,
                        SubTitle = setupBlogRequest.SubTitle,
                        Visible = setupBlogRequest.Visible
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new External.DTOs.Common.Models.Error() 
                        { 
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
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
                        return BadRequest(new External.DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
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
