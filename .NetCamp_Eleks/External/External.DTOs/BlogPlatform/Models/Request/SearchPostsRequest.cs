using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Request
{
    public class SearchPostsRequest
    {
        public string BlogUrl { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SearchText { get; set; }
    }
}
