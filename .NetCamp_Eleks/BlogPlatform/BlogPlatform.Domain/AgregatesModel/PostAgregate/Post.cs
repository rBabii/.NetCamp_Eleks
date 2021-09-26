using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.PostAgregate
{
    /// <summary>
    /// Post Objet - Contains All User Post Items
    /// </summary>
    public class Post
    {
        public int Id { get; set; }
        public List<PostItem> PostItems { get; set; }
        public void AddPostItem()
        {
            var _postItem = new PostItem();
            PostItems.Add(_postItem);
        }
    }
}
