using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Api.Models.Response
{
    public class SetupBlogResponse
    {
        public int BlogId { get; set; }
        public string BlogUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Visible { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
