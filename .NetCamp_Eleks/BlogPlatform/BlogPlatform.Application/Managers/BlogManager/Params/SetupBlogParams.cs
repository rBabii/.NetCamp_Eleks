using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Params
{
    public class SetupBlogParams
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string BlogUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public bool Visible { get; set; }
    }
}
