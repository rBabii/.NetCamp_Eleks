using External.DTOs.BlogPlatform.Models.Response.Childs;
using External.DTOs.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Response
{
    public class GetBlogListResponse
    {
        public List<GetBlogListItem> Blogs { get; set; }
        public Error Error { get; set; }
    }
}
