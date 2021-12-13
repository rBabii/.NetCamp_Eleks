using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using External.Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Result
{
    public class SearchBlogsResult : BaseResult
    {
        public List<Blog> Blogs { get; set; }
        public SearchBlogsResult(List<Blog> blogs, Error error = null)
            :base(error)
        {
            Blogs = blogs;
        }
    }
}
