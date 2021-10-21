using Result.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Result
{
    public class DeleteBlogResult : BaseResult
    {
        public DeleteBlogResult(Error error = null)
            :base(error)
        {

        }
    }
}
