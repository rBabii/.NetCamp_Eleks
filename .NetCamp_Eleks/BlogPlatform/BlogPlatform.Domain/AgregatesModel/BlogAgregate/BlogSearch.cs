using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.BlogAgregate
{
    public class BlogSearch
    {
        public int BlogSearchId { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public string FullBlogText { get; set; }
    }
}
