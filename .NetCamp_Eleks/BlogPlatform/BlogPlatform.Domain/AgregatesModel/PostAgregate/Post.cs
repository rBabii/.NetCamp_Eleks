using BlogPlatform.Domain.AgregatesModel.BlogAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.PostAgregate
{
    public class Post
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DatePosted { get; set; }
        public bool Blocked { get; set; }
        public bool Visible { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string HeaderContent { get; set; }
        public string MainContent { get; set; }
        public string FooterContent { get; set; }
        public string ImageName { get; set; }
        public string PreviewText { get; set; }
        public PostSearch PostSearch { get; set; }
    }
}
