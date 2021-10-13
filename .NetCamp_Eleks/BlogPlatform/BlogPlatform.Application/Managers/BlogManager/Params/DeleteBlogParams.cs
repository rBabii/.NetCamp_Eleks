using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Application.Managers.BlogManager.Params
{
    public class DeleteBlogParams
    {
        [Required]
        public int UserId { get; set; }
    }
}
