using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.BlogAgregate
{
    /// <summary>
    /// Represent a one Blog Service (Contain all blogs page from all Users that setuped theirs blogs here)
    /// </summary>
    public class Blog
    {
        public int Id { get; set; }
        public List<BlogItem> UserBlogItems { get; set; }
        /// <summary>
        /// Used to Add new Blog Item (Blog page) for User
        /// </summary>
        public void AddBlogItem()
        {
            var _blogItem = new BlogItem();
            UserBlogItems.Add(_blogItem);
        }
    }
}
