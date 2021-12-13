using External.DTOs.BlogPlatform.Models.Response.Childs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Response
{
    public class GetPostListResponse
    {
        public List<GetPostListItem> Posts { get; set; }
    }
}
