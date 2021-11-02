using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Response.Childs
{
    public class GetSingleBlogPagePostItem
    {
        public DateTime DateCreated { get; set; }
        public DateTime DatePosted { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string HeaderContent { get; set; }
        public string MainContent { get; set; }
        public string FooterContent { get; set; }
    }
}
