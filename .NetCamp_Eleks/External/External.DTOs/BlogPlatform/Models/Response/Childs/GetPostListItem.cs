using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Response.Childs
{
    public class GetPostListItem
    {
        public int PostId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string PreviewText { get; set; } 
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorImage { get; set; }
        public string PostMainImage { get; set; }
    }
}
