using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DTOs.BlogPlatform.Models.Request
{
    public class DeletePostRequest
    {
        public int PostId { get; set; }
    }
}
