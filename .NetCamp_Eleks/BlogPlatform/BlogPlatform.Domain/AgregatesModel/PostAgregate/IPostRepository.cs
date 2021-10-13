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
        Post AddOrUpdate(Post post);
        bool Delete(Post post);
    }
}
