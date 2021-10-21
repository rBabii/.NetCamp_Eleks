using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.BlogPlatform.Models.Request
{
    public class CreatePostRequest
    {
        public DateTime DatePosted { get; set; }
        public bool Visible { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string HeaderContent { get; set; }
        public string MainContent { get; set; }
        public string FooterContent { get; set; }
    }
}
