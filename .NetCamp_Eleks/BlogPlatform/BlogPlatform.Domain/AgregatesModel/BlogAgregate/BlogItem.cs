using BlogPlatform.Domain.AgregatesModel.PostAgregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.BlogAgregate
{
    /// <summary>
    /// Represent Single Blog Page that contains All Posts from one User
    /// </summary>
    public class BlogItem
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public Post Post { get; set; }
    }
}
