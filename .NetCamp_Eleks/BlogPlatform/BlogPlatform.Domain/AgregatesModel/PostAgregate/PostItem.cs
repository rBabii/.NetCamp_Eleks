using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Domain.AgregatesModel.PostAgregate
{
    /// <summary>
    /// Single Post Item
    /// </summary>
    public class PostItem
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string HeaderContent { get; set; }
        public string MainContent { get; set; }
        public string FooterContet { get; set; }
    }
}
