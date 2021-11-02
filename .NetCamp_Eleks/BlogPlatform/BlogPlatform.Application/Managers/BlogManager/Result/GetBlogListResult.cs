using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Result
{
    public class GetBlogListResult : BaseResult
    {
        public List<Blog> Blogs;
        public GetBlogListResult(List<Blog> blogs, Error error = null)
            :base(error)
        {
            Blogs = blogs;
        }
    }
}
