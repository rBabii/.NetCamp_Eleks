using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.DTOs.AttachmentService.Models.Response
{
    public class SaveSingleImageResponse
    {
        public int Key { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
    }
}
