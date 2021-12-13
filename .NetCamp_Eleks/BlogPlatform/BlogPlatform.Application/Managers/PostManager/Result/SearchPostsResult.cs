using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.PostManager.Result
{
    public class SearchPostsResult : BaseResult
    {
        public List<Post> Posts { get; set; }
        public SearchPostsResult(List<Post> posts, Error error = null)
            :base(error)
        {
            Posts = posts;
        }
    }
}
