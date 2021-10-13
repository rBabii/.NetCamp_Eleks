using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPlatform.Api.Models.Request
{
    public class DeletePostRequest
    {
        [Required]
        public int PostId { get; set; }
    }
}
