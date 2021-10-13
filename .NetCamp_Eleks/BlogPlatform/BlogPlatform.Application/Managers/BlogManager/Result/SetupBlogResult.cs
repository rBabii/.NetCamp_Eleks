using BlogPlatform.Application.Result;
using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Result
{
    public class SetupBlogResult : BaseResult
    {
        public Blog Blog { get; set; }
        public SetupBlogResult(Blog blog, Error error = null)
            : base (error)
        {
            Blog = blog;
        }
    }
}
