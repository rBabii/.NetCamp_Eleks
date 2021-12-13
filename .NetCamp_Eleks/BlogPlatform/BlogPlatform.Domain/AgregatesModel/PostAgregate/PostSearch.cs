using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.PostAgregate
{
    public class PostSearch
    {
        public int PostSearchId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string FullPostText { get; set; }
    }
}
