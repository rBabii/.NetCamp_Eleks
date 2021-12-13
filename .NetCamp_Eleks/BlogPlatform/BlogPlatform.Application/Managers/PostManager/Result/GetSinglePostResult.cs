using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Result
{
    public class GetSinglePostResult : BaseResult
    {
        public Post Post { get; set; }
        public GetSinglePostResult(Post post, Error error = null)
            : base(error)
        {
            Post = post;
        }
    }
}
