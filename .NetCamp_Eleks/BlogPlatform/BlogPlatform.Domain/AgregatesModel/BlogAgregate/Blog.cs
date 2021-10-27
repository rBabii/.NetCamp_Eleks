using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using BlogPlatform.Domain.AgregatesModel.UserAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.BlogAgregate
{
    public class Blog
    {
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string BlogUrl { get; set; }
        public IEnumerable<Post> Posts { get; }
        public DateTime DateCreated { get; set; }
        public bool Blocked { get; set; }
        public bool Visible { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
