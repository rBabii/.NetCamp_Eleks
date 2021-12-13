using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.PostAgregate
{
    public interface IPostRepository
    {
        IEnumerable<Post> Get();
        Post Get(int Id);
        IEnumerable<Post> GetByBlogId(int blogId);
        IEnumerable<Post> GetByBlogUrl(string blogUrl);
        Post AddOrUpdate(Post post);
        bool Delete(Post post);
        IEnumerable<Post> SearchPosts(int pageNumber = 1, int pageSize = 1, string blogUrl = "", string searchText = "", bool loadRelatedEntities = false);
    }
}
