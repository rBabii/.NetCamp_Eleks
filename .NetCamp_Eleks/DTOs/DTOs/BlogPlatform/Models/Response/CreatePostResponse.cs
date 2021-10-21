using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.BlogPlatform.Models.Response
{
    public class CreatePostResponse
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePosted { get; set; }
        public bool Visible { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string HeaderContent { get; set; }
        public string MainContent { get; set; }
        public string FooterContent { get; set; }
    }
}
