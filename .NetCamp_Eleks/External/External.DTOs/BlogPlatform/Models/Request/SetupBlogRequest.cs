using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Request
{
    public class SetupBlogRequest
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public bool Visible { get; set; }
    }
}
