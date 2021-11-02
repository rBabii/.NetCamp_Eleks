using BlogPlatform.Application.Managers.BlogManager.Params;
using BlogPlatform.Application.Managers.BlogManager.Result;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using External.Result.Base;
using External.Result.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager
{
    public class BlogManager
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public BlogManager(IBlogRepository blogRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public SetupBlogResult SetupBlog(SetupBlogParams setupBlogParams)
        {
            var validateErrorResult = ParamsValidator.Validate(setupBlogParams);
            if (validateErrorResult != null)
            {
                return new SetupBlogResult(null, validateErrorResult);
            }

            var blogWithUrl = _blogRepository.GetByUrl(setupBlogParams.BlogUrl);
            if (blogWithUrl != null)
            {
                return new SetupBlogResult(null, new Error($"Blog Url: {setupBlogParams.BlogUrl} already taken.", ErrorType.Validation));
            }

            var blogUser = _userRepository.GetByAuthResourceUserId(setupBlogParams.UserId);
            if (blogUser == null)
            {
                return new SetupBlogResult(null, new Error("Your Account is not fully Setuped.", ErrorType.Validation));
            }

            if (blogUser.Blog != null)
            {
                return new SetupBlogResult(null, new Error("Blog Page is allready setuped.", ErrorType.Validation));
            }



            var blog = _blogRepository.AddOrUpdate(new Blog()
            {
                UserId = blogUser.Id,
                BlogUrl = setupBlogParams.BlogUrl,
                DateCreated = DateTime.UtcNow,
                Title = setupBlogParams.Title,
                SubTitle = setupBlogParams.SubTitle,
                Visible = setupBlogParams.Visible
            });

            return new SetupBlogResult(blog);
        }

        public DeleteBlogResult DeleteBlog(DeleteBlogParams deleteBlogParams)
        {
            var validateErrorResult = ParamsValidator.Validate(deleteBlogParams);
            if (validateErrorResult != null)
            {
                return new DeleteBlogResult(validateErrorResult);
            }

            var blogUser = _userRepository.GetByAuthResourceUserId(deleteBlogParams.UserId);
            if(blogUser == null)
            {
                return new DeleteBlogResult(new Error("User is not fully setuped. Blog to delete Is not exist.", ErrorType.Validation));
            }

            var blog = _blogRepository.GetByUserId(blogUser.Id);
            if (blog == null)
            {
                return new DeleteBlogResult(new Error("Blog to delete Is not exist", ErrorType.Validation));
            }
            var res = _blogRepository.Delete(blog);
            if (!res)
            {
                return new DeleteBlogResult(new Error("Blog Delete Failed", ErrorType.SystemError));
            }
            return new DeleteBlogResult();
        }

        public GetBlogListResult GetBlogList()
        {
            var blogs = _blogRepository.Get().Where(b => b.Visible && !b.Blocked).ToList();
            if (blogs == null || blogs.Count < 1)
            {
                return new GetBlogListResult(blogs, new Error("Blog List is empty", ErrorType.Info));
            }
            return new GetBlogListResult(blogs);
        }

        public GetSingleBlogPageResult GetSingleBlogPage(GetSingleBlogPageParams getSingleBlogPageParams)
        {
            var validateErrorResult = ParamsValidator.Validate(getSingleBlogPageParams);
            if (validateErrorResult != null)
            {
                return new GetSingleBlogPageResult(null, validateErrorResult);
            }
            var blog = _blogRepository.GetByUrl(getSingleBlogPageParams.BlogUrl);
            if(blog == null)
            {
                return new GetSingleBlogPageResult(null, new Error("Blog Page Is not exist.", ErrorType.Validation));
            }
            return new GetSingleBlogPageResult(blog);
        }
    }
}
