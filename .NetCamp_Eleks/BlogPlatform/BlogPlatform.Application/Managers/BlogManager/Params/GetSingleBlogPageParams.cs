using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Params
{
    public class GetSingleBlogPageParams
    {
        [Required(ErrorMessage = "BlogUrl is Required.")]
        [RegularExpression("^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\\-]*[a-zA-Z0-9])\\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\\-]*[A-Za-z0-9])$", ErrorMessage = "Invalid Blog Url.")]
        public string BlogUrl { get; set; }
    }
}
