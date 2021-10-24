using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Result
{
    public class CreatePostResult : BaseResult
    {
        public Post Post { get; set; }
        public CreatePostResult(Post post, Error error = null)
            : base(error)
        {
            Post = post;
        }
    }
}
