using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Result
{
    public class GetSingleBlogPageResult : BaseResult
    {
        public Blog Blog { get; set; }
        public GetSingleBlogPageResult(Blog blog, Error error = null)
            : base(error)
        {
            Blog = blog;
        }
    }
}
