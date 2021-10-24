﻿using BlogPlatform.Application.Managers.PostManager.Params;
using BlogPlatform.Application.Managers.PostManager.Result;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using External.Result.Base;
using External.Result.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager
{
    public class PostManager
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        public PostManager(IBlogRepository blogRepository, IPostRepository postRepository)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
        }

        public CreatePostResult CreatePost(CreatePostParams createPostParams)
        {
            var validateErrorResult = ParamsValidator.Validate(createPostParams);
            if (validateErrorResult != null)
            {
                return new CreatePostResult(null, validateErrorResult);
            }
            var blog = _blogRepository.GetByUserId(createPostParams.UserId);
            if(blog == null)
            {
                return new CreatePostResult(null, new Error("Blog is not Setuped. Need to setup blog before post creating.", ErrorType.Validation));
            }
            var post = _postRepository.AddOrUpdate(new Post() 
            {
                BlogId = blog.BlogId,
                DateCreated = DateTime.UtcNow,
                DatePosted = createPostParams.DatePosted < DateTime.UtcNow ? DateTime.UtcNow : createPostParams.DatePosted,
                Visible = createPostParams.Visible,
                Title = createPostParams.Title,
                SubTitle = createPostParams.SubTitle,
                HeaderContent = createPostParams.HeaderContent,
                MainContent = createPostParams.MainContent,
                FooterContent = createPostParams.FooterContent
            });
            return new CreatePostResult(post);
        }

        public DeletePostResult DeletePost(DeletePostParams deletePostParams)
        {
            var validateErrorResult = ParamsValidator.Validate(deletePostParams);
            if (validateErrorResult != null)
            {
                return new DeletePostResult(validateErrorResult);
            }

            var blog = _blogRepository.GetByUserId(deletePostParams.UserId);
            if(blog == null)
            {
                return new DeletePostResult(new Error("Blog is not setuped. Posts does not exist.", ErrorType.Validation));
            }
            var posts = _postRepository.GetByBlogId(blog.BlogId);
            if(posts == null || posts.ToList().Count < 1)
            {
                return new DeletePostResult( new Error("Blog does not contains any posts", ErrorType.Validation));
            }
            var index = blog.Posts.ToList().FindIndex(p => p.PostId == deletePostParams.PostId);
            if(index < 0)
            {
                return new DeletePostResult(new Error("Post to delete does not exist or does not belong to your blog", ErrorType.Validation));
            }
            var post = posts.ToList()[index];
            var res = _postRepository.Delete(post);
            if (!res)
            {
                return new DeletePostResult(new Error("Post Delete Failed", ErrorType.SystemError));
            }
            return new DeletePostResult();
        }
    }
}
