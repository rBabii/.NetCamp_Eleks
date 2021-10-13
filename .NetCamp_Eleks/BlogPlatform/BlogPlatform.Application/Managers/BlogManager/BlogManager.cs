using BlogPlatform.Application.Managers.BlogManager.Params;
using BlogPlatform.Application.Managers.BlogManager.Result;
using BlogPlatform.Application.Result;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using BlogPlatform.Domain.AgregatesModel.PostAgregate;
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
        public BlogManager(IBlogRepository blogRepository, IPostRepository postRepository)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
        }

        public SetupBlogResult SetupBlog(SetupBlogParams setupBlogParams)
        {
            var blog = _blogRepository.AddOrUpdate(new Blog()
            {
                UserId = setupBlogParams.UserId,
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
            var blog = _blogRepository.GetByUserId(deleteBlogParams.UserId);
            if(blog == null)
            {
                return new DeleteBlogResult(new Error("Blog to delete Is not exist"));
            }
            var res = _blogRepository.Delete(blog);
            if (!res)
            {
                return new DeleteBlogResult(new Error("Blog Delete Failed"));
            }
            return new DeleteBlogResult();
        }
    }
}
