using BlogPlatform.Application.Managers.PostManager.Params;
using BlogPlatform.Application.Managers.PostManager.Result;
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

namespace BlogPlatform.Application.Managers.PostManager
{
    public class PostManager
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostManager(IBlogRepository blogRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public CreatePostResult CreatePost(CreatePostParams createPostParams)
        {
            var validateErrorResult = ParamsValidator.Validate(createPostParams);
            if (validateErrorResult != null)
            {
                return new CreatePostResult(null, validateErrorResult);
            }

            var blogUser = _userRepository.GetByAuthResourceUserId(createPostParams.UserId);
            if(blogUser == null)
            {
                return new CreatePostResult(null, new Error("User is not Setuped. Need to setup User before post creating.", ErrorType.Validation));
            }
            if(blogUser.Blog == null)
            {
                return new CreatePostResult(null, new Error("Blog is not Setuped. Need to setup blog before post creating.", ErrorType.Validation));
            }
            var post = _postRepository.AddOrUpdate(new Post() 
            {
                BlogId = blogUser.Blog.BlogId,
                DateCreated = DateTime.UtcNow,
                DatePosted = createPostParams.DatePosted < DateTime.UtcNow ? DateTime.UtcNow : createPostParams.DatePosted,
                Visible = createPostParams.Visible,
                Title = createPostParams.Title,
                SubTitle = createPostParams.SubTitle,
                HeaderContent = createPostParams.HeaderContent,
                MainContent = createPostParams.MainContent,
                FooterContent = createPostParams.FooterContent,
                PreviewText = createPostParams.PreviewText,
                ImageName = createPostParams.PostImageName
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

        public GetPostListResult GetPostList(GetPostListParams getPostListParams)
        {
            var validateErrorResult = ParamsValidator.Validate(getPostListParams);
            if (validateErrorResult != null)
            {
                return new GetPostListResult(null, validateErrorResult);
            }

            var posts = _postRepository.GetByBlogUrl(getPostListParams.BlogUrl);
            return new GetPostListResult(posts);
        }

        public GetSinglePostResult GetSinglePost(GetSinglePostParams getSinglePostParams)
        {
            var validateErrorResult = ParamsValidator.Validate(getSinglePostParams);
            if (validateErrorResult != null)
            {
                return new GetSinglePostResult(null, validateErrorResult);
            }

            var post = _postRepository.Get(getSinglePostParams.PostId);
            if(post == null)
            {
                return new GetSinglePostResult(null, new Error("Post not founded.", ErrorType.Validation));
            }
            return new GetSinglePostResult(post);
        }

        public SearchPostsResult SearchPosts(SearchPostsParams searchPostsParams)
        {
            var validateErrorResult = ParamsValidator.Validate(searchPostsParams);
            if (validateErrorResult != null)
            {
                return new SearchPostsResult(null, validateErrorResult);
            }

            var posts = _postRepository.SearchPosts(searchPostsParams.PageNumber, searchPostsParams.PageSize, searchPostsParams.BlogUrl, searchPostsParams.SearchText, searchPostsParams.LoadRelatedEntities);
            if(posts == null || posts.Count() < 1)
            {
                return new SearchPostsResult(null, new Error("Posts Is not founded.", ErrorType.Validation));
            }
            
            return new SearchPostsResult(posts.ToList());
        }
    }
}
