using BlogPlatform.Application.Managers.PostManager;
using BlogPlatform.Application.Managers.PostManager.Params;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
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
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return StatusCode(500, "Invalid Auth Token.");
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
                        FooterContent = createPostRequest.FooterContent,
                        PreviewText = createPostRequest.PreviewText,
                        PostImageName = createPostRequest.PostImageName
                    });
                    if (!result.IsValid && !result.CanContinue)
                    {
                        return BadRequest(new External.DTOs.Common.Models.Error()
                        {
                            ErrorMessages = result.Error.ErrorMessages,
                            ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                        });
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
                return StatusCode(500, "Invalid Auth Token.");
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
            try
            {
                var ClaimUserID = HttpContext.User.Claims.FirstOrDefault(c => c.Properties.FirstOrDefault().Value == JwtRegisteredClaimNames.Sub);
                if (ClaimUserID == null)
                {
                    return StatusCode(500, "Invalid Auth Token.");
                }
                if (Int32.TryParse(ClaimUserID.Value, out int UserID) && UserID != 0)
                {
                    var result = _postManager.DeletePost( new DeletePostParams() 
                    {
                        UserId = UserID,
                        PostId = deletePostRequest.PostId
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
            catch (Exception ex)
            {
                return StatusCode(500, "Delete Blog failed.");
            }
        }


        [Route("api/[controller]/GetPostList")]
        [HttpPost]
        public IActionResult GetPostList(GetPostListRequest getPostListRequest)
        {
            var result = _postManager.GetPostList(new GetPostListParams() 
            {
                BlogUrl = getPostListRequest.BlogUrl
            });

            if (!result.IsValid && !result.CanContinue)
            {   
                return BadRequest(new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                });
            }
            return Ok(new GetPostListResponse() 
            {
                Posts = result.Posts.Select(p => new External.DTOs.BlogPlatform.Models.Response.Childs.GetPostListItem 
                {
                    PostId = p.PostId,
                    AuthorFirstName = p.Blog.User.FirstName,
                    AuthorImage = p.Blog.User.ImageName,
                    AuthorLastName = p.Blog.User.LastName,
                    DatePosted = p.DatePosted,
                    PostMainImage = p.ImageName,
                    PreviewText = p.PreviewText,
                    SubTitle = p.Title,
                    Title = p.Title
                }).ToList()
            });
        }

        [Route("api/[controller]/GetSinglePost")]
        [HttpPost]
        public IActionResult GetSinglePost(GetSinglePostRequest getSinglePostRequest)
        {
            var result = _postManager.GetSinglePost(new GetSinglePostParams() 
            {
                PostId = getSinglePostRequest.PostId
            });

            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                });
            }
            return Ok(new GetSinglePostResponse() 
            {
                AuthorFirstName = result.Post.Blog.User.FirstName,
                DatePosted = result.Post.DatePosted,
                AuthorImage = result.Post.Blog.User.ImageName,
                AuthorLastName = result.Post.Blog.User.LastName,
                PostFooter = result.Post.FooterContent,
                PostHeader = result.Post.HeaderContent,
                PostMainContent = result.Post.MainContent,
                PostMainImage = result.Post.ImageName,
                SubTitle = result.Post.SubTitle,
                Title = result.Post.Title,
                BlogUrl = result.Post.Blog.BlogUrl
            });
        }

        [Route("api/[controller]/SearchPosts")]
        [HttpPost]
        public IActionResult SearchPosts(SearchPostsRequest searchPostsRequest)
        {
            var result = _postManager.SearchPosts(new SearchPostsParams()
            {
                BlogUrl = searchPostsRequest.BlogUrl,
                SearchText = searchPostsRequest.SearchText,
                LoadRelatedEntities = true,
                PageNumber = searchPostsRequest.PageNumber,
                PageSize = searchPostsRequest.PageSize
            });

            if (!result.IsValid && !result.CanContinue)
            {
                return BadRequest(new External.DTOs.Common.Models.Error()
                {
                    ErrorMessages = result.Error.ErrorMessages,
                    ErrorType = (External.DTOs.Common.Enums.ErrorType)result.Error.ErrorType
                });
            }
            return Ok(new SearchPostsResponse()
            {
                SearchPostItems = result.Posts.Select(p => new External.DTOs.BlogPlatform.Models.Response.Childs.SearchPostItem
                {
                    PostId = p.PostId,
                    AuthorFirstName = p.Blog.User.FirstName,
                    AuthorImage = p.Blog.User.ImageName,
                    AuthorLastName = p.Blog.User.LastName,
                    DatePosted = p.DatePosted,
                    PostMainImage = p.ImageName,
                    PreviewText = p.PreviewText,
                    SubTitle = p.Title,
                    Title = p.Title
                }).ToList()
            });
        }
    }
}
