using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.BlogPlatform.Models.Request
{
    public class SaveUserImageRequest
    {
        public int AuthResourceUserId { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }
    }
}
